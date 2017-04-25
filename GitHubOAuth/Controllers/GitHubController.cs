using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace GitHubOAuth.Controllers
{
    public class GitHubController : Controller
    {
        string clientId = "<client id>";
        string clientSecret = "<client secret>";
        string token = "";
        string githubUrl = "https://api.github.com";

        // GET: GitHub
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Auth()
        {
            string code = Request.QueryString["code"];
            string state = Request.QueryString["state"];

            //Get Token for Auth Code
            string tokenURL = "https://github.com/login/oauth/access_token";
            string redirectUrl = "https://localhost:44300/GitHub/Auth";

            using (WebClient client = new WebClient())
            {

                byte[] response =
                client.UploadValues(tokenURL, new NameValueCollection()
                {
                    {"client_id", clientId},
                    {"client_secret", clientSecret},
                    {"code", code},
                    {"redirect_uri", redirectUrl},
                    {"state", state}
               });

                string result = System.Text.Encoding.UTF8.GetString(response);
                NameValueCollection collection = HttpUtility.ParseQueryString(result);
                Session["AccessToken"] = collection["access_token"];
            }

            return View();
        }

        public ActionResult GetRepos()
        {
            if (Session["AccessToken"] == null)
            {
                RedirectToAction("Login");
            }

            var repoUrl = "/user/repos?access_token={0}";
            string url = string.Format(githubUrl + repoUrl, Session["AccessToken"]);
            using(WebClient client = new WebClient())
            {
                string result = client.DownloadString(url);
            }
            return View();
        }
    }
}