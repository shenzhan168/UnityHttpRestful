using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

using RestHttpAPI;
using MiniJSON;

public class reqTest : MonoBehaviour {



	string url = "https://127.0.0.1:5000/user/newUser";
	public string postData="";

	public void TestAPI()
	{   
		//print ("TestAPI");
		//StartCoroutine ("IETestAPI");
		//StartCoroutine ("IEAuth");

		//ActionTest ( ()=>{ print(url);} );
		HttpAPI.Instance.ipAdr = "https://127.0.0.1:5000";
//		HttpAPI.Instance.UserName = "b";
//		HttpAPI.Instance.Password = "123";
		//HttpAPI.Instance.Get ("/hello",(resp,error)=>{print(resp+" "+error);});
		//HttpAPI.Instance.Post ("/hello",(resp,error)=>{print(resp+" "+error);});
		//HttpAPI.Instance.GetAuth ("/test",(resp,error)=>{print(resp+" "+error);});

		//new user 
//		Dictionary<string,string> UserDic = new Dictionary<string, string> ();
//		UserDic["username"]="you";
//		UserDic["password"]="123";
//		string str = Json.Serialize(UserDic);
//		HttpAPI.Instance.Post ("/user/newUser", str, (resp,error) => {
//			print (resp + " " + error);}
//		);

		//check code
		Dictionary<string,string> UserDic = new Dictionary<string, string> ();
		UserDic["appname"]="aote2";
		UserDic["channel"]="haha";
		UserDic["exCode"]="SGUAYCNYRB";
		string str = Json.Serialize(UserDic);
		HttpAPI.Instance.Post("/checkCode", str, (resp,error) => {
			print (resp + " " + error);}
		);

		//generateCode
//		Dictionary<string,string> UserDic = new Dictionary<string, string> ();
//		UserDic["appname"]="aote2";
//		UserDic["channel"]="haha";
//		UserDic["count"]="180";
//		string str = Json.Serialize(UserDic);
//		HttpAPI.Instance.PostAuth("/generateCode", str, (resp,error) => {
//			print (resp + " " + error);}
//		);


		//get apps
//		HttpAPI.Instance.GetAuth ("/getexcodeinfo", (resp,error) => {
//			print (resp + " " + error);
//		});
	}




	void ActionTest(Action callback)
	{
		callback ();
	}

	IEnumerator IETestAPI()
	{
		byte[] post_data;  
		Dictionary<string,string> dic = new Dictionary<string,string> ();

		string str_params="";  
		str_params ="{\"username\":\"c\",\"password\":\"123\"}";   
		print (str_params);
		post_data = System.Text.UTF8Encoding.UTF8.GetBytes(str_params);  

//		WWWForm	form  = new WWWForm();
//		form.AddField("u","A");

		dic.Add("Content-Type", "application/json");

		WWW www = new WWW(url,post_data,dic);
		yield return www;

		if (www.error != null) {
			print (www.error);
		} else {
			print ("test"+www.text);
		}


	}

	IEnumerator IEAuth()
	{
		string url = "https://127.0.0.1:5000/test";


		Dictionary<string,string> dic = new Dictionary<string,string> ();
		dic.Add("Content-Type", "application/json");
		dic.Add("Authorization", "Basic " + System.Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes("b:123")) );
		
		WWW www = new WWW(url,null,dic);
		yield return www;
		
		if (www.error != null) {
			print (www.error);
		} else {
			print ("test"+www.text);
		}

	}
}
