using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using Firebase.Database;
using BaseRPG_V1;

public class FirebaseManager : BaseSingleton<FirebaseManager>
{
    // 인증용 객체.
    private FirebaseAuth auth = null;

    // 로그인 완료 된 유저의 객체.
    private FirebaseUser user = null;

    // 파이어베이스 db.
    private DatabaseReference db = null;

    // 파이어베이스 초기화.
    public void Initialization()
    {
        // 인증용 객체 초기화.
        auth = FirebaseAuth.DefaultInstance;        

        SignInGuest();
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

            // db 초기화.
            db = FirebaseDatabase.DefaultInstance.RootReference;

            // 유저 객체 생성.
            user = task.Result.User;

            JsonToSign();
        });
    }

    // 게임 접속 시 유저데이터 가져옴.
    public void ParseToUserData()
    {
        db.Child(user.UserId).GetValueAsync().ContinueWith(task => 
        {
            if (task.IsCanceled)
            {
                return;
            }

            if (task.IsFaulted)
            {
                return;
            }

            var data = task.Result;
        });
    }

    // 회원가입 시 파베에 넣음.
    public void JsonToSign()
    {
        UserData userData = new UserData();
        string json = JsonUtility.ToJson(userData);        

        db.Child(user.UserId).SetRawJsonValueAsync(json).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                return;
            }

            if (task.IsFaulted)
            {
                return;
            }
        });
    }
}
