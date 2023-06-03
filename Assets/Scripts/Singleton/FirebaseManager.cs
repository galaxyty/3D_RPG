using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using Firebase.Database;
using BaseRPG_V1;

public class FirebaseManager : BaseSingleton<FirebaseManager>
{
    // 로그인 사용 할 객체.
    private FirebaseAuth auth = null;

    // 로그인 완료 된 유저의 객체.
    private FirebaseUser user = null;

    // 파이어베이스 db.
    private DatabaseReference db = null;

    // 유저데이터.
    private UserData m_UserData = new UserData();

    // 유저데이터 레퍼런스.
    public UserData UserData
    {
        get
        {
            return m_UserData;
        }
    }

    // 파이어베이스 초기화.
    public void Initialization()
    {
        // 로그인 객체 초기화.
        auth = FirebaseAuth.DefaultInstance;

        // 유저 상태가 바뀔 때 마다 실행되는 이벤트 핸들러.
        auth.StateChanged += OnState;

        SignInGuest();
    }

    // 로그인 완료 된 유저 객체 넣음.
    private void OnState(object sender, EventArgs e)
    {
        if (auth.CurrentUser != user)
        {
            user = auth.CurrentUser;
        }
    }

    // 게스트 로그인.
    public void SignInGuest()
    {
        Debug.Log("로그인 중...");

        auth.SignInAnonymouslyAsync().ContinueWith(task => 
        {
            Debug.Log("로그인 완료");

            if (task.IsCanceled)
            {
                Debug.Log("게스트 로그인 취소");
                return;
            }

            if (task.IsFaulted)
            {
                Debug.Log("게스트 로그인 실패 : " + task.Exception);
                return;
            }

            if (task.IsCompleted)
            {
                // db 초기화.
                db = FirebaseDatabase.DefaultInstance.RootReference;

                ParseToUserData();
            }
        });
    }

    // 유저 데이터 파싱.
    public void ParseToUserData()
    {
        db.Child(Constants.kDB_USER).Child(user.UserId).GetValueAsync().ContinueWith(task => 
        {
            if (task.IsCanceled)
            {
                return;
            }

            if (task.IsFaulted)
            {
                return;
            }

            if (task.IsCompleted)
            {
                var data = task.Result;

                // null 체크.
                if (data == null)
                {
                    return;
                }

                // data를 json으로 가져오고, 그 json을 UserData로 변형하여 직렬화 시킨다.
                var json = JsonUtility.FromJson<UserData>(data.GetRawJsonValue());

                // List 직렬화.
                foreach (var snapshot in data.Children)
                {
                    // 아이템 데이터 파싱.
                    ParseToItemData(snapshot);
                }
            }
        });
    }

    // 아이템 데이터 추가.
    public void JsonToItemData(ItemData item)
    {
        // null 체크.
        if (item == null)
        {
            return;
        }

        // 이미 소지하고 있는 아이템인지 확인.
        ItemData data = m_UserData.ITEM.Find(foundData => foundData.INDEX == item.INDEX);

        if (data == null)
        {
            // 소지하지 않았으면 갯수 1로 넣음.
            item.QTY = 1;
        }
        else
        {
            // 소지했으면 현재 갯수에 +1.
            item.QTY = data.QTY + 1;
        }

        // json으로 변경.
        string json = JsonUtility.ToJson(item);

        db.Child(Constants.kDB_USER).Child(user.UserId).Child("ITEM").Child(item.INDEX.ToString()).SetRawJsonValueAsync(json).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                return;
            }

            if (task.IsFaulted)
            {
                return;
            }

            if (task.IsCompleted)
            {
                // 해당 인덱스 아이템이 처음으로 들어온것인지 확인.
                if (data == null)
                {
                    // 처음 들어오면 리스트 추가.
                    m_UserData.ITEM.Add(item);
                    return;
                }                

                // 이미 들어온 아이템이면 갯수만 늘려준다.
                m_UserData.ITEM.Find(foundData => foundData.INDEX == item.INDEX).QTY += 1;
            }
        });
    }

    // 아이템 데이터 파싱.
    private void ParseToItemData(DataSnapshot snapshot)
    {
        // null 체크.
        if (snapshot == null)
        {
            return;
        }

        foreach (var data in snapshot.Children)
        {
            // data를 json으로 가져오고, 그 json을 ItemData로 변형하여 직렬화 시킨다.
            ItemData itemJson = JsonUtility.FromJson<ItemData>(data.GetRawJsonValue());

            // 변형한 직렬화를 리스트에 추가.
            m_UserData.ITEM.Add(itemJson);
        }
    }
}
