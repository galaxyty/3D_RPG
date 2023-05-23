using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using BaseRPG_V1;

public class FirebaseManager : BaseSingleton<FirebaseManager>
{
    // 인증용 객체.
    private FirebaseAuth auth = null;

    // 로그인 완료 된 유저의 객체.
    private AuthResult user = null;

    // 파이어베이스 초기화.
    public void Initialization()
    {
        // 인증용 객체 초기화.
        auth = FirebaseAuth.DefaultInstance;

        SignInGuest();
    }

    // 게스트 로그인.
    private void SignInGuest()
    {
        auth.SignInAnonymouslyAsync().ContinueWith(task => 
        {
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

            // 유저 객체 생성.
            user = task.Result;
        });
    }
}
