using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenWeb : MonoBehaviour
{
    public void UrlOpen(string url){
#if UNITY_WEBGL
    Application.ExternalEval("window.open(\"" + url + "\")");
#else
    Application.OpenURL(url);
#endif
    }
}
