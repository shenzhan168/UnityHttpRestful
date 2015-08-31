using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


/// <summary>
/// Http Request SDK 
/// </summary>
namespace RestHttpAPI
{
public class HttpAPI : MonoBehaviour {
    
		private static HttpAPI _instacne=null;
		private HttpAPI()
		{
		}

		public static HttpAPI Instance{
			get{
				if(_instacne == null )
				{
					Debug.LogError("Awake error");
				}
				return _instacne ;
			}
		}

		void Awake () {
			DontDestroyOnLoad (gameObject);
			HttpAPI._instacne = gameObject.GetComponent<HttpAPI> ();   
				
			JsonDic.Add("Content-Type", "application/json");

			AuthDic.Add("Content-Type", "application/json");
			string NameAndPw = UserName + ":" + Password;
			AuthDic.Add("Authorization", "Basic " + System.Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(NameAndPw)) );

		}

		public string ipAdr="https://127.0.0.1:5000";
		public string ver = "";
		Dictionary<string,string> AuthDic = new Dictionary<string,string> ();  // auth header
		Dictionary<string,string> JsonDic = new Dictionary<string,string> ();  // json parser header

		public string UserName="";
		public string Password="";


		/// <summary>
		/// Get Method with callback
		/// </summary>
		/// <param name="url">URL.</param>
		/// <param name="callBack">Call back.</param>
		public void Get(string url,Action<string,string> callBack=null)
		{
			StartCoroutine ( IEHttpGet(this.ipAdr+url,callBack));
		}
		IEnumerator IEHttpGet(string fullUrl,Action<string,string> callBack)
		{
			//print ("get url " + fullUrl);
			WWW www = new WWW(fullUrl);
			yield return www;
			
			if (www.error != null) {
				Debug.LogError (www.error);
			} 

			if(callBack != null)
			   callBack (www.text,www.error);
		}

		/// <summary>
		/// Get Method with callback and  BaseAuth
		/// </summary>
		/// <param name="url">URL.</param>
		/// <param name="callBack">Call back.</param>
		public void GetAuth(string url,Action<string,string> callBack=null)
		{
			StartCoroutine (IEGetAuth(url,callBack));
		}
		IEnumerator IEGetAuth(string url,Action<string,string> callBack=null)
		{

			string fullUrl = this.ipAdr + url;


			WWW www = new WWW(fullUrl,null,AuthDic);
			yield return www;
			
			if (www.error != null) {
				Debug.LogError ("error:"+www.error);
			} 
			
			if(callBack != null)
				callBack (www.text,www.error);
		}

		/// <summary>
		/// Post the specified url and callBack.
		/// </summary>
		/// <param name="url">URL.</param>
		/// <param name="callBack">Call back.</param>
		public void Post(string url,Action<string,string> callBack=null)
		{
			StartCoroutine (IEPost (url, callBack));
		}
		IEnumerator IEPost(string url,Action<string,string> callBack=null)
		{
			string fullUrl = this.ipAdr + url;

			WWWForm wf = new WWWForm ();
			wf.AddField ("K","v");
			
			WWW www = new WWW(fullUrl,wf);
			yield return www;
			
			if (www.error != null) {
				Debug.LogError ("error:"+www.error);
			}
			
			if(callBack != null)
				callBack (www.text,www.error);
		}

		/// <summary>
		/// Post the specified url, data and callBack.
		/// </summary>
		/// <param name="url">URL.</param>
		/// <param name="data">Data.</param>
		/// <param name="callBack">Call back.</param>
		public void Post(string url,string data, Action<string,string> callBack=null)
		{
			StartCoroutine (IEPost(url,data,callBack));
		}
		IEnumerator IEPost(string url,string data, Action<string,string> callBack=null)
		{
			string fullUrl = this.ipAdr + url;
			
			byte[] post_data;  
			post_data = System.Text.UTF8Encoding.UTF8.GetBytes(data);  
			
			WWW www = new WWW(fullUrl,post_data,JsonDic);
			yield return www;
			
			if (www.error != null) {
				Debug.LogError ("error:"+www.error);
			} 
			if(callBack != null)
				callBack (www.text,www.error);
		}


		/// <summary>
		/// Posts the auth.
		/// </summary>
		/// <param name="url">URL.</param>
		/// <param name="data">Data.</param>
		/// <param name="callBack">Call back.</param>
		public void PostAuth(string url,string data, Action<string,string> callBack=null)
		{
			StartCoroutine (IEPostAuth(url,data,callBack));
		}
		IEnumerator IEPostAuth(string url,string data, Action<string,string> callBack=null)
		{
			string fullUrl = this.ipAdr + url;
			
			byte[] post_data;  
			post_data = System.Text.UTF8Encoding.UTF8.GetBytes(data);  
			
			WWW www = new WWW(fullUrl,post_data,AuthDic);
			yield return www;
			
			if (www.error != null) {
				Debug.LogError ("error:"+www.error);
			}
			
			if(callBack != null)
				callBack (www.text,www.error);
		}
	
}

}
