using PCMS_Library.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCMS_Library.BAL
{
    public class pcms_BAL
    {
        pcsm_DAL objDAL = new pcsm_DAL();
        public pcms_admin getAdminCredential(pcms_admin _Admin)
        {
            return objDAL.userCredential(_Admin);
        }
        public List<pcms_campaign> ListCampaign()
        {
            return objDAL.ListCampaign();
        }
        public List<pcms_design> getDesignTemplates()
        {
            return objDAL.getDesignTemplates();
        }

        public List<pcms_content> getContentTemplates()
        {
            return objDAL.getContentTemplates();
        }
        public List<pcms_popup> getPopupTemplates()
        {
            return objDAL.getPopupTemplates();
        }
        public List<pcms_rules> getListofRules()
        {
            return objDAL.getListofRules();
        }
        public string AddCampain(pcms_campaign _campaign)
        {
            return objDAL.InsertCampaignData(_campaign);
        }

        public List<pcms_content> getContents()
        {
            return objDAL.getContents();
        }

        public string AddContent(pcms_content _Content)
        {
            return objDAL.AddContent(_Content);
        }

        public string DeleteContent(int query)
        {
            return objDAL.DeleteContent(query);
        }

        public pcms_content EditContent(int query)
        {
            return objDAL.EditContent(query);
        }
        public string UpdateContent(pcms_content _content)
        {
            return objDAL.UpdateContent(_content);
        }

        public string AddWebsite(pcms_List list)
        {
            return objDAL.AddWebsite(list);
        }

        public string UpdateUrlList(string weburl, string INSERTED_BY,DateTime INSERTED_DATE,int TS_CNT,string url)
        {
            return objDAL.UpdateUrlList(weburl,INSERTED_BY,INSERTED_DATE,TS_CNT,url);
        }

        public List<pcms_rules> getUrlList(string url)
        {
            return objDAL.getUrlList(url);

        }

        public string DeleteRules(string query)
        {
            return objDAL.DeleteRules(query);
        }


        public List<pcms_design> getDesign()
        {
            return objDAL.getDesign();
        }

        public string AddDesign(pcms_design _Design)
        {
            return objDAL.AddDesign(_Design);
        }

        public string DeleteDesign(int query)
        {
            return objDAL.DeleteDesign(query);
        }

        public pcms_design EditDesign(int query)
        {
            return objDAL.EditDesign(query);
        }
        public string UpdateDesign(pcms_design _Design)
        {
            return objDAL.UpdateDesign(_Design);
        }

        public List<pcms_popup> getPopup()
        {
            return objDAL.getpopUp();
        }

        public string Addpopup(pcms_popup _Popup)
        {
            return objDAL.AddpopUp(_Popup);
        }

        public string DeletePopup(int query)
        {
            return objDAL.DeletepopUp(query);
        }

        public pcms_popup EditPopup(int query)
        {
            return objDAL.EditpopUp(query);
        }
        public string UpdatePopup(pcms_popup _Popup)
        {
            return objDAL.UpdatepopUp(_Popup);
        }

        public List<pcms_rules> getURLBYID(string query)
        {
            return objDAL.getURLBYID(query);
        }
        public List<pcms_country_list> GetCountryList()
        {
            return objDAL.GetCountryList();
        }
        public string updateRules(pcms_rules rule)
        {
            return objDAL.updateRules(rule);
        }

        public List<pcms_campaign> getCampaignByid(string query)
        {
            return objDAL.getCampaignByid(query);
        }

        public string UpdateCamaign(pcms_campaign campaign)
        {
            return objDAL.UpdateCamaign(campaign);
        }

        public List<pcms_website> getWebURL()
        {
            return objDAL.getWebURL();
        }
        public string SaveURL(string rulesuri, string weburlitem,string usr)
        {
            return objDAL.SaveURL(rulesuri, weburlitem,usr);
        }

        public List<pcms_campaign> GetCampaignList(string query)
        {
            return objDAL.GetCampaignList(query);
        }

    }
}
