using PCMS.Models;
using PCMS_Library;
using PCMS_Library.BAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace PCMS.Controllers
{
    public class HomeController : Controller
    {
        pcms_BAL objBAL = new pcms_BAL();
        public ActionResult Index()
        {
            try
            {
                TempData["url"] = "";
                if (Session["userKey"] == null)
                {
                    return RedirectToAction("Index", "Account", null);
                }
                else
                {
                    var result = objBAL.ListCampaign();
                    return View(result);
                }
            }
            catch (Exception ex)
            {
                ErrorLog obj = new ErrorLog();
                obj.LogError(ex);
                return null;
            }
        }


        public ActionResult AddCampaign()
        {
            TempData["url"] = "";
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                ErrorLog obj = new ErrorLog();
                obj.LogError(ex);
                return null;
            }
        }
        [HttpPost]
        public ActionResult AddCampaign(FormCollection collection)
        {

            if (Session["userKey"] != null)
            {
                try
                {

                    pcms_campaign campaign = new pcms_campaign();
                    campaign.CAMPAIGN_NAME = collection["campaign_name"].ToString();
                    string[] s = collection["startdate"].ToString().Split('/');
                    campaign.CAMPAIGN_START_DATE = Convert.ToDateTime(s[2] + "/" + s[0] + "/" + s[1]);
                    string[] s1 = collection["endDate"].ToString().Split('/');
                    campaign.CAMPAIGN_END_DATE = Convert.ToDateTime(s1[2] + "/" + s1[0] + "/" + s1[1]);
                    campaign.CAMPAIGN_DESCRIPTION = Convert.ToString(collection["Description"]);
                    campaign.CAMPAIGN_URL = collection["campaignurl"].ToString();
                    campaign.CAMPAIGN_DESIGN_ID = collection["designlist"] == "" ? 0 : Convert.ToInt16(collection["designlist"]);
                    campaign.CAMPAIGN_CONTENT_ID = collection["contentlist"] == "" ? 0 : Convert.ToInt16(collection["contentlist"]);
                    campaign.CAMPAIGN_POPUP_ID =Convert.ToInt16(Convert.ToString(collection["popuplist"]));
                    campaign.CAMPAIGN_CONTAINER_ID = Convert.ToString(collection["containerid"]);
                    campaign.CAMPAIGN_STATUS = Convert.ToString(collection["campaingstatus"]);
                    campaign.CAMPAIGN_REDIRECT_URL = Convert.ToString(collection["redirecturl"]);
                    campaign.CAMPAIGN_RULE_ID = Convert.ToInt16(collection["rulelister"]);
                    campaign.CAMPAIGN_POPUP_STATUS = Convert.ToString(collection["popup"]);
                    campaign.INSERTED_BY = Session["userKey"].ToString();
                    campaign.INSERTED_DATE = DateTime.Now;
                    campaign.TS_CNT = 0;
                    var campaigner = objBAL.AddCampain(campaign);

                    if (campaigner.Contains("Inserted"))
                    {
                        ViewBag.Message = "Campaign Inserted";
                    }
                    else if (campaigner.Contains("Duplicate"))
                    {
                        ViewBag.Message = "Campaign name must be unique";
                    }
                    else
                    {
                        ViewBag.Message = "Something went wrong";
                    }
                    return View();
                }
                catch (Exception ex)
                {
                    ErrorLog obj = new ErrorLog();
                    obj.LogError(ex);
                    return null;
                }
            }
            else
            {
                return RedirectToAction("Index", "Account", null);
            }
        }

        public ActionResult DesignTemplates()
        {

            try
            {
                var DesignTemplates = objBAL.getDesignTemplates();
                return View(DesignTemplates);
            }
            catch (Exception ex)
            {
                ErrorLog obj = new ErrorLog();
                obj.LogError(ex);
                return null;
            }
        }

        public ActionResult ContentTemplates()
        {
            try
            {
                var ContentTemplates = objBAL.getContentTemplates();
                return View(ContentTemplates);
            }
            catch (Exception ex)
            {
                ErrorLog obj = new ErrorLog();
                obj.LogError(ex);
                return null;
            }

        }

        public ActionResult PopupTemplates()
        {
            try
            {
                var PopupTemplates = objBAL.getPopupTemplates();
                return View(PopupTemplates);
            }
            catch (Exception ex)
            {
                ErrorLog obj = new ErrorLog();
                obj.LogError(ex);
                return null;
            }
        }

        public ActionResult ListofRules()
        {
            try
            {
                var RuleList = objBAL.getListofRules();
                return View(RuleList);
            }
            catch (Exception ex)
            {
                ErrorLog obj = new ErrorLog();
                obj.LogError(ex);
                return null;
            }
        }
        public ActionResult Content()
        {
            TempData["url"] = "";
            try
            {
                var Result = objBAL.getContents();
                return View(Result);
            }
            catch (Exception ex)
            {
                ErrorLog obj = new ErrorLog();
                obj.LogError(ex);
                return null;
            }
        }
        public ActionResult AddContent()
        {
            TempData["url"] = "";
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                ErrorLog obj = new ErrorLog();
                obj.LogError(ex);
                return null;
            }
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult AddContent(pcms_content content, FormCollection col)
        {
            try
            {
                content.INSERTED_DATE = DateTime.Now;
                content.TS_CNT = 0;
                content.INSERTED_BY = Session["userKey"].ToString();
                var res = objBAL.AddContent(content);
                if (res.Contains("Inserted"))
                {
                    return RedirectToAction("Content");
                }
                else if (res.Contains("Duplicate"))
                {
                    ViewBag.Error = "Content Name must be unique";
                    return View();
                }
                else
                {
                    ViewBag.Error = "Something went wrong while adding content";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ErrorLog obj = new ErrorLog();
                obj.LogError(ex);
                return null;
            }

        }
        public ActionResult EditContent(string query)
        {
            try
            {
                var Resultset = objBAL.EditContent(Convert.ToInt16(query));
                return View(Resultset);
            }
            catch (Exception ex)
            {
                ErrorLog obj = new ErrorLog();
                obj.LogError(ex);
                return null;
            }
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult EditContent(pcms_content _content)
        {
            try
            {
                _content.TS_CNT = 1;
                _content.UPDATED_DATE = DateTime.Now;
                _content.UPDATED_BY = Convert.ToString(Session["userKey"]);
                var Resultset = objBAL.UpdateContent(_content);
                if (Resultset.Contains("Updated"))
                {
                    return RedirectToAction("Content", "Home", null);
                }
                else
                {
                    return RedirectToAction("Content", "Home", null);
                }
            }
            catch (Exception ex)
            {
                ErrorLog obj = new ErrorLog();
                obj.LogError(ex);
                return null;
            }
        }
        public ActionResult DeleteContent(string query)
        {
            try
            {
                var res = objBAL.DeleteContent(Convert.ToInt16(query));
                if (res.Contains("Deleted"))
                {
                    return RedirectToAction("Content", "Home", null);
                }
                else
                {
                    return RedirectToAction("Content", "Home", null);
                }
            }
            catch (Exception ex)
            {
                ErrorLog obj = new ErrorLog();
                obj.LogError(ex);
                return null;
            }
        }

        public ActionResult Website()
        {
            TempData["url"] = "";
            return View();
        }
        [HttpPost]
        public ActionResult Website(pcms_List website)
        {
            try
            {
                website.url[0].INSERTED_DATE = DateTime.Now;
                website.url[0].TS_CNT = 0;
                website.url[0].INSERTED_BY = Session["userKey"].ToString();
                var KEY = objBAL.AddWebsite(website);
                if (KEY.Contains("Duplicate"))
                {
                    ViewBag.Message = "Url already exist";
                    return View();
                }
                else
                {
                    TempData["url"] = website.url[0].WEBSITE_URL;
                    if (KEY != null)
                    {
                        if (website.url[0].WEBSITE_SITEMAP_URL != "" && website.url[0].WEBSITE_SITEMAP_URL != null)
                        {
                            var baseurl = website.url[0].WEBSITE_SITEMAP_URL;
                            /*Create a new instance of the System.Net Webclient*/
                            WebClient wc = new WebClient();
                            /*Set the Encodeing on the Web Client*/
                            wc.Encoding = System.Text.Encoding.UTF8;
                            /* Download the document as a string*/
                            string reply = wc.DownloadString(baseurl);
                            /*Create a new xml document*/
                            XmlDocument urldoc = new XmlDocument();
                            /*Load the downloaded string as XML*/
                            urldoc.LoadXml(reply);
                            /*Create an list of XML nodes from the url nodes in the sitemap*/
                            XmlNodeList xnList = urldoc.GetElementsByTagName("url");
                            /*Loops through the node list and prints the values of each node*/
                            var inserted = "";
                            int i = 1, j = 1;
                            foreach (XmlNode node in xnList)
                            {
                                string weburl = website.url[0].WEBSITE_URL.ToString();
                                string INSERTED_BY = Session["userKey"].ToString();
                                DateTime INSERTED_DATE = DateTime.Now;
                                int TS_CNT = 0;
                                inserted = objBAL.UpdateUrlList(weburl, INSERTED_BY, INSERTED_DATE, TS_CNT, node["loc"].InnerText);
                                if (inserted.Contains("Inserted"))
                                {
                                    i = i + 1;
                                }
                                j = j + 1;
                            }
                            if (i > 0)
                            {
                                if (i == j)
                                {
                                    TempData["Key"] = KEY;
                                    return RedirectToAction("URLList");
                                }
                                else
                                {
                                    ViewBag.Message = i + " url/s has been added out of " + j + " url/s";
                                }

                            }
                            else
                            {
                                ViewBag.Message = "Something went wrong while adding url/s";
                            }
                        }
                        else
                        {
                            TempData["Key"] = KEY;
                            return RedirectToAction("URLList");
                        }
                    }
                    else
                    {
                        ViewBag.Message = "Something went wrong while adding url/s";
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog obj = new ErrorLog();
                obj.LogError(ex);
                ViewBag.Message = ex.Message;
            }
            return View();
        }
        public ActionResult URLList(pcms_rules rules)
        {
            string url = "";
            if (!string.IsNullOrEmpty(Convert.ToString(TempData["url"])))
            {
                url = TempData["url"].ToString();
            }
            else
            {
                url = "";
            }
            var ResultSet = objBAL.getUrlList(url);
            TempData.Keep();
            return View(ResultSet);
        }
        public ActionResult DeleteRules(string query)
        {
            var res = objBAL.DeleteRules(query);
            if (res.Contains("Deleted"))
            {
                return RedirectToAction("URLList", "Home", null);
            }
            else
            {
                return RedirectToAction("URLList", "Home", null);
            }
        }

        public ActionResult Design()
        {
            TempData["url"] = "";
            var Res = objBAL.getDesign();
            return View(Res);
        }
        public ActionResult AddDesign()
        {
            TempData["url"] = "";
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                ErrorLog obj = new ErrorLog();
                obj.LogError(ex);
                return null;
            }
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult AddDesign(pcms_design Design, FormCollection col)
        {
            try
            {
                Design.INSERTED_DATE = DateTime.Now;
                Design.TS_CNT = 0;
                Design.INSERTED_BY = Session["userKey"].ToString();
                var res = objBAL.AddDesign(Design);
                if (res.Contains("Inserted"))
                {
                    return RedirectToAction("Design");
                }
                else if (res.Contains("Duplicate"))
                {
                    ViewBag.Error = "design Name must be unique";
                    return View();
                }
                else
                {
                    ViewBag.Error = "Something went wrong while adding Design";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ErrorLog obj = new ErrorLog();
                obj.LogError(ex);
                return null;
            }

        }

        public ActionResult EditDesign(string query)
        {
            try
            {
                var Resultset = objBAL.EditDesign(Convert.ToInt16(query));
                return View(Resultset);
            }
            catch (Exception ex)
            {
                ErrorLog obj = new ErrorLog();
                obj.LogError(ex);
                return null;
            }
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult EditDesign(pcms_design _design)
        {
            try
            {
                _design.TS_CNT = 1;
                _design.UPDATED_DATE = DateTime.Now;
                _design.UPDATED_BY = Convert.ToString(Session["userKey"]);
                var Resultset = objBAL.UpdateDesign(_design);
                if (Resultset.Contains("Updated"))
                {
                    return RedirectToAction("Design", "Home", null);
                }
                else
                {
                    return RedirectToAction("Design", "Home", null);
                }
            }
            catch (Exception ex)
            {
                ErrorLog obj = new ErrorLog();
                obj.LogError(ex);
                return null;
            }
        }

        public ActionResult DeleteDesign(string query)
        {
            try
            {
                var res = objBAL.DeleteDesign(Convert.ToInt16(query));
                if (res.Contains("Deleted"))
                {
                    return RedirectToAction("Design", "Home", null);
                }
                else
                {
                    return RedirectToAction("Design", "Home", null);
                }
            }
            catch (Exception ex)
            {
                ErrorLog obj = new ErrorLog();
                obj.LogError(ex);
                return null;
            }
        }

        public ActionResult PopUp()
        {
            TempData["url"] = "";
            var Res = objBAL.getPopup();
            return View(Res);
        }
        public ActionResult AddPopup()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                ErrorLog obj = new ErrorLog();
                obj.LogError(ex);
                return null;
            }
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult AddPopup(pcms_popup Popup, FormCollection col)
        {
            try
            {
                Popup.INSERTED_DATE = DateTime.Now;
                Popup.TS_CNT = 0;
                Popup.INSERTED_BY = Session["userKey"].ToString();
                var res = objBAL.Addpopup(Popup);
                if (res.Contains("Inserted"))
                {
                    return RedirectToAction("Popup");
                }
                else if (res.Contains("Duplicate"))
                {
                    ViewBag.Error = "Popup Name must be unique";
                    return View();
                }
                else
                {
                    ViewBag.Error = "Something went wrong while adding Popu";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ErrorLog obj = new ErrorLog();
                obj.LogError(ex);
                return null;
            }

        }

        public ActionResult EditPopup(string query)
        {
            try
            {
                var Resultset = objBAL.EditPopup(Convert.ToInt16(query));
                return View(Resultset);
            }
            catch (Exception ex)
            {
                ErrorLog obj = new ErrorLog();
                obj.LogError(ex);
                return null;
            }
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult EditPopup(pcms_popup _popup)
        {
            try
            {
                _popup.TS_CNT = 1;
                _popup.UPDATED_DATE = DateTime.Now;
                _popup.UPDATED_BY = Convert.ToString(Session["userKey"]);
                var Resultset = objBAL.UpdatePopup(_popup);
                if (Resultset.Contains("Updated"))
                {
                    return RedirectToAction("Popup", "Home", null);
                }
                else
                {
                    return RedirectToAction("Popup", "Home", null);
                }
            }
            catch (Exception ex)
            {
                ErrorLog obj = new ErrorLog();
                obj.LogError(ex);
                return null;
            }
        }

        public ActionResult DeletePopup(string query)
        {
            try
            {
                var res = objBAL.DeletePopup(Convert.ToInt16(query));
                if (res.Contains("Deleted"))
                {
                    return RedirectToAction("Popup", "Home", null);
                }
                else
                {
                    return RedirectToAction("Popup", "Home", null);
                }
            }
            catch (Exception ex)
            {
                ErrorLog obj = new ErrorLog();
                obj.LogError(ex);
                return null;
            }
        }

        public ActionResult Rules(string query)
        {
            TempData["url"] = "";
            if (Session["userKey"] != null)
            {
                try
                {
                    var result = objBAL.getURLBYID(query);                   
                    return View(result);
                }
                catch (Exception ex)
                {
                    ErrorLog obj = new ErrorLog();
                    obj.LogError(ex);
                    return null;
                }
            }
            else
            {
                return RedirectToAction("Index", "Account", null);
            }
        }

        [HttpPost]
        public ActionResult Rules(FormCollection formData)
        {
            if (Session["userKey"] != null)
            {
                try
                {
                    pcms_rules rules = new pcms_rules();
                    rules.RULE_APPLIED_URL = TempData["RULE_APPLIED_URL"].ToString();
                    rules.RULE_ID = Convert.ToInt16(TempData["QUERY"].ToString());
                    rules.RULE_NAME = formData["rule-name"];
                    rules.RULE_LOCATION = formData["location"];
                    rules.RULE_PARAMETER_KEY_VALUE = formData["parameter_list"];
                    rules.RULE_USER_TYPE = formData["user"];
                    rules.INSERTED_BY = Session["userKey"].ToString();
                    rules.INSERTED_DATE = DateTime.Now;
                    rules.WEBSITE_URL = formData["WEBSITE_URL"].ToString();
                    rules.TS_CNT = 0;
                    rules.RULE_ISDELETED = false;
                    var Data = objBAL.updateRules(rules);
                    var result = TempData["Filter"];
                    TempData.Keep();
                    if (Data.Contains("Inserted"))
                    {
                        ViewBag.Message = "New rule has been added";
                        return View(result);
                    }
                    else if (Data.Contains("Duplicate"))
                    {
                        ViewBag.Message = "Rule name must be unique";
                        return View(result);
                    }
                    else
                    {
                        ViewBag.Message = "Something went wrong while adding rule";
                        return View(result);
                    }
                }


                catch (Exception ex)
                {
                    ErrorLog obj = new ErrorLog();
                    obj.LogError(ex);
                    return null;

                }
            }
            else
            {
                return RedirectToAction("Index", "Account", null);
            }
        }
        public ActionResult GetCountryList()
        {
            var res = objBAL.GetCountryList();
            return View(res);
        }

        public ActionResult EditCampaign(string query)
        {
            var res = objBAL.getCampaignByid(query);
            return View(res);
        }
        [HttpPost]
        public ActionResult EditCampaign(FormCollection collection)
        {
            try
            {
                pcms_campaign campaign = new pcms_campaign();
                campaign.CAMPAIGN_NAME = collection["campaign_name"].ToString();
                string[] s = collection["startdate"].ToString().Split('/');
                campaign.CAMPAIGN_START_DATE = Convert.ToDateTime(s[2] + "/" + s[0] + "/" + s[1]);
                string[] s1 = collection["endDate"].ToString().Split('/');
                campaign.CAMPAIGN_END_DATE = Convert.ToDateTime(s1[2] + "/" + s1[0] + "/" + s1[1]);
                campaign.CAMPAIGN_DESCRIPTION = Convert.ToString(collection["Description"]);
                campaign.CAMPAIGN_URL = collection["campaignurl"].ToString();
                campaign.CAMPAIGN_DESIGN_ID = collection["designlist"] == "" ? 0 : Convert.ToInt16(collection["designlist"]);
                campaign.CAMPAIGN_CONTENT_ID = collection["contentlist"] == "" ? 0 : Convert.ToInt16(collection["contentlist"]);
                campaign.CAMPAIGN_POPUP_ID =Convert.ToInt16(Convert.ToString(collection["popuplist"]));
                campaign.CAMPAIGN_CONTAINER_ID = Convert.ToString(collection["containerid"]);
                campaign.CAMPAIGN_STATUS = Convert.ToString(collection["campaingstatus"]);
                campaign.CAMPAIGN_REDIRECT_URL = Convert.ToString(collection["redirecturl"]);
                campaign.CAMPAIGN_RULE_ID = Convert.ToInt16(collection["rulelister"]);
                campaign.CAMPAIGN_ID =Convert.ToInt16(collection["pkcamaign"]);
                campaign.CAMPAIGN_POPUP_STATUS = Convert.ToString(collection["popup"]);
                campaign.INSERTED_BY = Session["userKey"].ToString();
                campaign.INSERTED_DATE = DateTime.Now;
                campaign.TS_CNT = 0;
                var campaigner = objBAL.UpdateCamaign(campaign);
                if (campaigner.Contains("Updated"))
                {
                    ViewBag.Message = "Campaign Updated";
                }
                else if (campaigner.Contains("Duplicate"))
                {
                    ViewBag.Message = "Campaign name must be unique";
                }
                else
                {
                    ViewBag.Message = "Something went wrong";
                }
                return RedirectToAction("Index", "Home", null);
            }
            catch (Exception ex)
            {
                ErrorLog obj = new ErrorLog();
                obj.LogError(ex);
                return null;
            }
        }


        public ActionResult AddURI()
        {
            TempData["url"] = "";
            if (Session["userKey"] != null)
            {
                try
                {
                    var resultset = objBAL.getWebURL();
                    return View(resultset.ToList());
                }
                catch (Exception ex)
                {
                    ErrorLog obj = new ErrorLog();
                    obj.LogError(ex);
                    return null;
                }
            }
            else
            {
                return RedirectToAction("Index", "Account", null);
            }
        }
        [HttpPost]
        public ActionResult AddURI(string rulesuri, string weburlitem)
        {
            try
            {
                TempData["url"] = weburlitem;
                string url = objBAL.SaveURL(weburlitem, rulesuri, Session["userKey"].ToString());
                if (url.Contains("inserted"))
                {
                    return RedirectToAction("URLList");
                }
                else
                {
                    TempData["Error"] = "Something went wrong";
                    return RedirectToAction("URLList");
                }
            }
            catch (Exception ex)
            {
                ErrorLog obj = new ErrorLog();
                obj.LogError(ex);
                return null;
            }
        }

        public ActionResult GetCampaignList(string query)
        {
            if (Session["userKey"] != null)
            {
                var Details = objBAL.GetCampaignList(query);
                return View(Details);
            }
            else
            {
                return RedirectToAction("Index", "Account", null);
            }
        }


    }
}
