using pcsm_Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCMS_Library.DAL
{
    public class pcsm_DAL
    {

        public pcms_admin userCredential(pcms_admin _admin)
        {
            try
            {
                pcms_admin _adminCredential = new pcms_admin();
                PCMS_DbContext context = new PCMS_DbContext();
                var res = context.pcms_admin.Single(admin => admin.ADMIN_USERNAME == _admin.ADMIN_USERNAME && admin.ADMIN_PASSWORD == _admin.ADMIN_PASSWORD);
                _adminCredential.ADMIN_ID = res.ADMIN_ID;
                return _adminCredential;
            }
            catch (Exception ex)
            {
                Utilities objUtilities = new Utilities();
                objUtilities.LogError(ex);
                return null;
            }
        }
        public List<pcms_campaign> ListCampaign()
        {
            try
            {
                using (PCMS_DbContext _pcmsDbContext = new PCMS_DbContext())
                {
                    var campaignlst = (_pcmsDbContext.pcms_campaign
                        .Select(cam => new
                        {
                            PK_CAMPAIGN = cam.PK_CAMPAIGN,
                            CAMPAIGN_ID = cam.CAMPAIGN_ID,
                            CAMPAIGN_NAME = cam.CAMPAIGN_NAME,
                            CAMPAIGN_START_DATE = cam.CAMPAIGN_START_DATE,
                            CAMPAIGN_END_DATE = cam.CAMPAIGN_END_DATE,
                            CAMPAIGN_STATUS = cam.CAMPAIGN_STATUS
                        }).ToList()
                              .Select(x => new pcms_campaign()
                              {
                                  PK_CAMPAIGN = x.PK_CAMPAIGN,
                                  CAMPAIGN_ID = x.CAMPAIGN_ID,
                                  CAMPAIGN_NAME = x.CAMPAIGN_NAME,
                                  CAMPAIGN_START_DATE = x.CAMPAIGN_START_DATE,
                                  CAMPAIGN_END_DATE = x.CAMPAIGN_END_DATE,
                                  CAMPAIGN_STATUS = x.CAMPAIGN_STATUS
                              }).OrderByDescending(y => y.CAMPAIGN_START_DATE)).ToList();

                    if (campaignlst.Count > 0)
                        return campaignlst;
                    else
                        return new List<pcms_campaign>();
                }
            }
            catch (Exception ex)
            {
                Utilities objUtilities = new Utilities();
                objUtilities.LogError(ex);
                return null;
            }
        }
        public List<pcms_design> getDesignTemplates()
        {
            try
            {
                using (PCMS_DbContext _pcmsDbContext = new PCMS_DbContext())
                {
                    var designList = (_pcmsDbContext.pcms_design
                        .Select(cam => new
                        {
                            DESIGN_ID = cam.DESIGN_ID,
                            DESIGN_NAME = cam.DESIGN_NAME,
                            DESIGN_DESCRIPTION = cam.DESIGN_DESCRIPTION,
                        }).ToList()
                              .Select(x => new pcms_design()
                              {
                                  DESIGN_ID = x.DESIGN_ID,
                                  DESIGN_NAME = Cryptographys.Decrypt(x.DESIGN_NAME),
                                  DESIGN_DESCRIPTION = Cryptographys.Decrypt(x.DESIGN_DESCRIPTION)
                              }).OrderByDescending(y => y.INSERTED_DATE)).ToList();
                    if (designList.Count > 0)
                        return designList;
                    else
                        return new List<pcms_design>();
                }
            }
            catch (Exception ex)
            {
                Utilities objUtilities = new Utilities();
                objUtilities.LogError(ex);
                return null;
            }
        }
        public List<pcms_content> getContentTemplates()
        {
            try
            {
                using (PCMS_DbContext _pcmsDbContext = new PCMS_DbContext())
                {
                    var content = (_pcmsDbContext.pcms_content
                        .Select(cam => new
                        {
                            CONTENT_ID = cam.CONTENT_ID,
                            CONTENT_NAME = cam.CONTENT_NAME,
                            CONTENT_DESCRIPTION = cam.CONTENT_DESCRIPTION,
                        }).ToList()
                              .Select(x => new pcms_content()
                              {
                                  CONTENT_ID = x.CONTENT_ID,
                                  CONTENT_NAME = Cryptographys.Decrypt(x.CONTENT_NAME),
                                  CONTENT_DESCRIPTION = Cryptographys.Decrypt(x.CONTENT_DESCRIPTION)
                              }).OrderByDescending(y => y.INSERTED_DATE)).ToList();
                    if (content.Count > 0)
                        return content;
                    else
                        return new List<pcms_content>();
                }
            }
            catch (Exception ex)
            {
                Utilities objUtilities = new Utilities();
                objUtilities.LogError(ex);
                return null;
            }
        }
        public List<pcms_popup> getPopupTemplates()
        {
            try
            {
                using (PCMS_DbContext _pcmsDbContext = new PCMS_DbContext())
                {
                    var popupDesign = (_pcmsDbContext.pcms_popup
                        .Select(cam => new
                        {
                            POPUP_ID = cam.POPUP_ID,
                            POPUP_NAME = cam.POPUP_NAME,
                            POPUP_DESCRIPTION = cam.POPUP_DESCRIPTION,
                        }).ToList()
                              .Select(x => new pcms_popup()
                              {
                                  POPUP_ID = x.POPUP_ID,
                                  POPUP_NAME = Cryptographys.Decrypt(x.POPUP_NAME),
                                  POPUP_DESCRIPTION = Cryptographys.Decrypt(x.POPUP_DESCRIPTION)
                              }).OrderByDescending(y => y.INSERTED_DATE)).ToList();
                    if (popupDesign.Count > 0)
                        return popupDesign;
                    else
                        return new List<pcms_popup>();
                }
            }
            catch (Exception ex)
            {
                Utilities objUtilities = new Utilities();
                objUtilities.LogError(ex);
                return null;
            }
        }
        public List<pcms_rules> getListofRules()
        {
            try
            {
                using (PCMS_DbContext _pcmsDbContext = new PCMS_DbContext())
                {
                    var rules = (_pcmsDbContext.pcms_rules
                        .Select(cam => new
                        {
                            RULE_ID = cam.RULE_ID,
                            RULE_NAME = cam.RULE_NAME,
                            RULE_DESCRIPTION = cam.RULE_DESCRIPTION,
                        }).ToList()
                              .Select(x => new pcms_rules()
                              {
                                  RULE_ID = x.RULE_ID,
                                  RULE_NAME = Cryptographys.Decrypt(x.RULE_NAME),
                                  RULE_DESCRIPTION = Cryptographys.Decrypt(x.RULE_DESCRIPTION)
                              }).OrderByDescending(y => y.INSERTED_DATE)).ToList();
                    if (rules.Count > 0)
                        return rules;
                    else
                        return new List<pcms_rules>();
                }
            }
            catch (Exception ex)
            {
                Utilities objUtilities = new Utilities();
                objUtilities.LogError(ex);
                return null;
            }
        }
        public string InsertCampaignData(pcms_campaign _campaign)
        {
            string Msg = "";
            PCMS_DbContext context = new PCMS_DbContext();

            try
            {
                if (!context.pcms_campaign.Any(u => u.CAMPAIGN_NAME == _campaign.CAMPAIGN_NAME))
                {
                    pcms_campaign cm = new pcms_campaign();
                    cm.PK_CAMPAIGN = Guid.NewGuid();
                    cm.CAMPAIGN_NAME = _campaign.CAMPAIGN_NAME;
                    cm.CAMPAIGN_DESCRIPTION = Cryptographys.Encrypt(_campaign.CAMPAIGN_DESCRIPTION);
                    cm.CAMPAIGN_START_DATE = Convert.ToDateTime(_campaign.CAMPAIGN_START_DATE);
                    cm.CAMPAIGN_END_DATE = Convert.ToDateTime(_campaign.CAMPAIGN_END_DATE);
                    cm.CAMPAIGN_STATUS = _campaign.CAMPAIGN_STATUS;
                    cm.CAMPAIGN_DESIGN_ID = _campaign.CAMPAIGN_DESIGN_ID;
                    cm.CAMPAIGN_CONTENT_ID = _campaign.CAMPAIGN_CONTENT_ID;
                    cm.CAMPAIGN_POPUP_STATUS = _campaign.CAMPAIGN_POPUP_STATUS;
                    cm.CAMPAIGN_POPUP_ID = _campaign.CAMPAIGN_POPUP_ID;
                    cm.CAMPAIGN_RULE_ID = _campaign.CAMPAIGN_RULE_ID;
                    cm.CAMPAIGN_URL = Cryptographys.Encrypt(_campaign.CAMPAIGN_URL);
                    cm.CAMPAIGN_REDIRECT_URL = Cryptographys.Encrypt(_campaign.CAMPAIGN_REDIRECT_URL);
                    cm.CAMPAIGN_CONTAINER_ID = _campaign.CAMPAIGN_CONTAINER_ID;
                    cm.INSERTED_BY = _campaign.INSERTED_BY;
                    cm.INSERTED_DATE = _campaign.INSERTED_DATE;
                    cm.TS_CNT = _campaign.TS_CNT;
                    context.AddTopcms_campaign(cm);
                    context.SaveChanges();
                    Msg = "Inserted";
                }
                else { Msg = "Duplicate"; }
                return Msg;
            }
            catch (Exception ex)
            {
                Utilities objUtilities = new Utilities();
                objUtilities.LogError(ex);
                Msg = "Something went wrong";
                return null;
            }
        }

        public List<pcms_content> getContents()
        {
            try
            {
                using (PCMS_DbContext _pcmsDbContext = new PCMS_DbContext())
                {
                    var content = (_pcmsDbContext.pcms_content
                        .Select(cam => new
                        {
                            CONTENT_ID = cam.CONTENT_ID,
                            CONTENT_NAME = cam.CONTENT_NAME,
                            CONTENT_DESCRIPTION = cam.CONTENT_DESCRIPTION,
                        }).ToList()
                              .Select(x => new pcms_content()
                              {
                                  CONTENT_ID = x.CONTENT_ID,
                                  CONTENT_NAME = Cryptographys.Decrypt(x.CONTENT_NAME),
                                  CONTENT_DESCRIPTION = Cryptographys.Decrypt(x.CONTENT_DESCRIPTION)
                              }).OrderByDescending(y => y.INSERTED_DATE)).ToList();
                    if (content.Count > 0)
                        return content;
                    else
                        return new List<pcms_content>();
                }
            }
            catch (Exception ex)
            {
                Utilities objUtilities = new Utilities();
                objUtilities.LogError(ex);
                return null;
            }
        }

        public string AddContent(pcms_content _Content)
        {
            string Msg = "";
            try
            {
                PCMS_DbContext context = new PCMS_DbContext();
                if (!context.pcms_campaign.Any(u => u.CAMPAIGN_NAME == _Content.CONTENT_NAME))
                {
                    pcms_content cn = new pcms_content();
                    cn.CONTENT_PK = Guid.NewGuid();
                    cn.CONTENT_NAME = Cryptographys.Encrypt(_Content.CONTENT_NAME);
                    cn.CONTENT_DESCRIPTION = Cryptographys.Encrypt(_Content.CONTENT_DESCRIPTION);
                    cn.CONTENT_HTML = Cryptographys.Encrypt(_Content.CONTENT_HTML);
                    cn.INSERTED_BY = _Content.INSERTED_BY;
                    cn.INSERTED_DATE = _Content.INSERTED_DATE;
                    cn.CONTENT_ISDELETED = _Content.CONTENT_ISDELETED;
                    cn.TS_CNT = _Content.TS_CNT;
                    context.AddTopcms_content(cn);
                    context.SaveChanges();
                    Msg = "Inserted";
                }
                else { Msg = "Duplicate"; }
                return Msg;
            }
            catch (Exception ex)
            {
                Utilities objUtilities = new Utilities();
                objUtilities.LogError(ex);
                Msg = "Something went wrong";
                return null;
            }
        }

        public string DeleteContent(int query)
        {
            string Msg = "";
            try
            {
                PCMS_DbContext context = new PCMS_DbContext();
                pcms_content content = context.pcms_content.Single(x => x.CONTENT_ID == query);
                context.pcms_content.DeleteObject(content);
                context.SaveChanges();
                Msg = "Deleted";
                return Msg;
            }
            catch (Exception ex)
            {
                Utilities objUtilities = new Utilities();
                objUtilities.LogError(ex);
                Msg = "Something went wrong";
                return null;
            }
        }

        public pcms_content EditContent(int query)
        {
            try
            {
                pcms_content _content = new pcms_content();
                PCMS_DbContext context = new PCMS_DbContext();
                var res = context.pcms_content.Single(content => content.CONTENT_ID == query);
                _content.CONTENT_ID = res.CONTENT_ID;
                _content.CONTENT_NAME = Cryptographys.Decrypt(res.CONTENT_NAME);
                _content.CONTENT_DESCRIPTION = Cryptographys.Decrypt(res.CONTENT_DESCRIPTION);
                _content.CONTENT_HTML = Cryptographys.Decrypt(res.CONTENT_HTML);
                _content.INSERTED_BY = res.INSERTED_BY;
                _content.INSERTED_DATE = res.INSERTED_DATE;
                _content.CONTENT_ISDELETED = res.CONTENT_ISDELETED;
                _content.TS_CNT = res.TS_CNT;
                return _content;
            }
            catch (Exception ex)
            {
                Utilities objUtilities = new Utilities();
                objUtilities.LogError(ex);
                return null;
            }
        }

        public string UpdateContent(pcms_content _content)
        {
            string Msg = "";
            try
            {
                PCMS_DbContext context = new PCMS_DbContext();
                pcms_content nUser = context.pcms_content.Single(u => u.CONTENT_ID == _content.CONTENT_ID);
                nUser.CONTENT_DESCRIPTION = Cryptographys.Encrypt(_content.CONTENT_DESCRIPTION);
                nUser.CONTENT_HTML = Cryptographys.Encrypt(_content.CONTENT_HTML);
                context.SaveChanges();
                Msg = "Updated";
                return Msg;
            }
            catch (Exception ex)
            {
                Utilities objUtilities = new Utilities();
                objUtilities.LogError(ex);
                Msg = "Something went wrong";
                return null;
            }
        }

        public string AddWebsite(pcms_List list)
        {
            string Msg = "";
            try
            {
                using (PCMS_DbContext _pcmsDbContext = new PCMS_DbContext())
                {
                    string url = list.url[0].WEBSITE_URL;
                    if (_pcmsDbContext.pcms_website.Any(u => u.WEBSITE_URL == url))
                    {
                        return Msg = "Duplicate";
                    }
                    else
                    {
                        pcms_website objWebsite = new pcms_website();
                        objWebsite.WEBSITE_URL = list.url[0].WEBSITE_URL;
                        objWebsite.WEBSITE_SITEMAP_URL = list.url[0].WEBSITE_SITEMAP_URL != null ? list.url[0].WEBSITE_SITEMAP_URL : "";
                        objWebsite.SDK_VERIFIED = Convert.ToBoolean(list.url[0].SDK_VERIFIED);
                        objWebsite.INSERTED_BY = list.url[0].INSERTED_BY;
                        objWebsite.INSERTED_DATE = list.url[0].INSERTED_DATE;
                        objWebsite.TS_CNT = list.url[0].TS_CNT;
                        objWebsite.WEBSITE_PK = Guid.NewGuid();
                        pcms_rules objRules = new pcms_rules();
                        string webmap = list.url[0].WEBSITE_URL != null ? Cryptographys.Encrypt(list.url[0].WEBSITE_URL) : "";
                        objRules.RULE_APPLIED_URL = webmap;
                        objRules.INSERTED_BY = list.url[0].INSERTED_BY;
                        objRules.INSERTED_DATE = list.url[0].INSERTED_DATE;
                        objRules.WEBSITE_URL = Cryptographys.Encrypt(list.url[0].WEBSITE_URL);
                        objRules.TS_CNT = list.url[0].TS_CNT;
                        objRules.RULE_PK = Guid.NewGuid();
                        _pcmsDbContext.AddTopcms_website(objWebsite);
                        _pcmsDbContext.AddTopcms_rules(objRules);
                        _pcmsDbContext.SaveChanges();
                        Msg = "Inserted";
                        return Msg;
                    }
                }

            }
            catch (Exception ex)
            {
                Utilities objUtilities = new Utilities();
                objUtilities.LogError(ex);
                Msg = "Something went wrong";
                return null;
            }
        }

        public string UpdateUrlList(string weburl, string insertby, DateTime insertdate, int tscnt, string url)
        {
            string Msg = "";
            try
            {
                PCMS_DbContext context = new PCMS_DbContext();
                pcms_rules objRules = new pcms_rules();
                objRules.RULE_APPLIED_URL = Cryptographys.Encrypt(url);
                objRules.INSERTED_BY = insertby;
                objRules.INSERTED_DATE = insertdate;
                objRules.WEBSITE_URL = Cryptographys.Encrypt(weburl);
                objRules.TS_CNT = tscnt;
                objRules.RULE_PK = Guid.NewGuid();
                context.AddTopcms_rules(objRules);
                context.SaveChanges();
                Msg = "Inserted";
                return Msg;
            }
            catch (Exception ex)
            {
                Utilities objUtilities = new Utilities();
                objUtilities.LogError(ex);
                Msg = "Something went wrong";
                return null;
            }

        }

        public List<pcms_rules> getUrlList(string url)
        {

            try
            {
                using (PCMS_DbContext _pcmsDbContext = new PCMS_DbContext())
                {
                    if (!string.IsNullOrEmpty(url))
                    {
                        string urls = Cryptographys.Encrypt(url);
                        var content = (_pcmsDbContext.pcms_rules
                            .Select(cam => new
                            {
                                RULE_ID = cam.RULE_ID,
                                RULE_APPLIED_URL = cam.RULE_APPLIED_URL,
                                WEBSITE_URL = cam.WEBSITE_URL
                            }).Where(y => y.WEBSITE_URL == urls).ToList()
                                  .Select(x => new pcms_rules()
                                  {
                                      RULE_ID = x.RULE_ID,
                                      RULE_APPLIED_URL = x.RULE_APPLIED_URL == "" ? "" : Cryptographys.Decrypt(x.RULE_APPLIED_URL),
                                      WEBSITE_URL = x.WEBSITE_URL == "" ? "" : Cryptographys.Decrypt(x.WEBSITE_URL)
                                  }).OrderByDescending(y => y.INSERTED_DATE)).ToList();
                        if (content.Count > 0)
                            return content;
                        else
                            return new List<pcms_rules>();
                    }
                    else
                    {
                        var content = (_pcmsDbContext.pcms_rules
                           .Select(cam => new
                           {
                               RULE_ID = cam.RULE_ID,
                               RULE_NAME = cam.RULE_NAME,
                               RULE_APPLIED_URL = cam.RULE_APPLIED_URL,
                               WEBSITE_URL = cam.WEBSITE_URL
                           }).ToList()
                                 .Select(x => new pcms_rules()
                                 {
                                     RULE_ID = x.RULE_ID,
                                     RULE_APPLIED_URL =  Cryptographys.Decrypt(x.RULE_APPLIED_URL),
                                     WEBSITE_URL =  Cryptographys.Decrypt(x.WEBSITE_URL)
                                 }).OrderByDescending(y => y.INSERTED_DATE)).ToList();
                        if (content.Count > 0)
                            return content;
                        else
                            return new List<pcms_rules>();
                    }
                }
            }
            catch (Exception ex)
            {
                Utilities objUtilities = new Utilities();
                objUtilities.LogError(ex);
                return null;
            }
        }

        public string DeleteRules(string query)
        {
            string Msg = "";
            try
            {
                PCMS_DbContext context = new PCMS_DbContext();
                pcms_rules rule = context.pcms_rules.Single(x => x.RULE_ID == Convert.ToInt16(query));
                context.pcms_rules.DeleteObject(rule);
                context.SaveChanges();
                Msg = "Deleted";
                return Msg;
            }
            catch (Exception ex)
            {
                Utilities objUtilities = new Utilities();
                objUtilities.LogError(ex);
                Msg = "Something went wrong";
                return null;
            }
        }

        public List<pcms_design> getDesign()
        {
            try
            {
                using (PCMS_DbContext _pcmsDbContext = new PCMS_DbContext())
                {
                    var designList = (_pcmsDbContext.pcms_design
                        .Select(cam => new
                        {
                            DESIGN_ID = cam.DESIGN_ID,
                            DESIGN_NAME = cam.DESIGN_NAME,
                            DESIGN_DESCRIPTION = cam.DESIGN_DESCRIPTION,
                        }).ToList()
                              .Select(x => new pcms_design()
                              {
                                  DESIGN_ID = x.DESIGN_ID,
                                  DESIGN_NAME = Cryptographys.Decrypt(x.DESIGN_NAME),
                                  DESIGN_DESCRIPTION = Cryptographys.Decrypt(x.DESIGN_DESCRIPTION)
                              }).OrderByDescending(y => y.INSERTED_DATE)).ToList();
                    if (designList.Count > 0)
                        return designList;
                    else
                        return new List<pcms_design>();
                }
            }
            catch (Exception ex)
            {
                Utilities objUtilities = new Utilities();
                objUtilities.LogError(ex);
                return null;
            }
        }

        public string AddDesign(pcms_design _Design)
        {
            string Msg = "";
            try
            {
                PCMS_DbContext context = new PCMS_DbContext();
                if (!context.pcms_campaign.Any(u => u.CAMPAIGN_NAME == _Design.DESIGN_NAME))
                {
                    pcms_design dn = new pcms_design();
                    dn.DESIGN_PK = Guid.NewGuid();
                    dn.DESIGN_NAME = Cryptographys.Encrypt(_Design.DESIGN_NAME);
                    dn.DESIGN_DESCRIPTION = Cryptographys.Encrypt(_Design.DESIGN_DESCRIPTION);
                    dn.DESIGN_HTML = Cryptographys.Encrypt(_Design.DESIGN_HTML);
                    dn.INSERTED_BY = _Design.INSERTED_BY;
                    dn.INSERTED_DATE = _Design.INSERTED_DATE;
                    dn.DESIGN_ISDELETED = _Design.DESIGN_ISDELETED;
                    dn.TS_CNT = _Design.TS_CNT;
                    context.AddTopcms_design(dn);
                    context.SaveChanges();
                    Msg = "Inserted";
                }
                else { Msg = "Duplicate"; }
                return Msg;
            }
            catch (Exception ex)
            {

                Utilities objUtilities = new Utilities();
                objUtilities.LogError(ex);
                Msg = "Something went wrong";
                return null;
            }
        }

        public string DeleteDesign(int query)
        {
            string Msg = "";
            try
            {
                PCMS_DbContext context = new PCMS_DbContext();
                pcms_design content = context.pcms_design.Single(x => x.DESIGN_ID == query);
                context.pcms_design.DeleteObject(content);
                context.SaveChanges();
                Msg = "Deleted";
                return Msg;
            }
            catch (Exception ex)
            {
                Utilities objUtilities = new Utilities();
                objUtilities.LogError(ex);
                Msg = "Something went wrong";
                return null;
            }
        }

        public pcms_design EditDesign(int query)
        {
            try
            {
                pcms_design _design = new pcms_design();
                PCMS_DbContext context = new PCMS_DbContext();
                var res = context.pcms_design.Single(content => content.DESIGN_ID == query);
                pcms_design dn = new pcms_design();
                _design.DESIGN_ID = res.DESIGN_ID;
                _design.DESIGN_NAME = Cryptographys.Decrypt(res.DESIGN_NAME);
                _design.DESIGN_DESCRIPTION = Cryptographys.Decrypt(res.DESIGN_DESCRIPTION);
                _design.DESIGN_HTML = Cryptographys.Decrypt(res.DESIGN_HTML);
                _design.INSERTED_BY = res.INSERTED_BY;
                _design.INSERTED_DATE = res.INSERTED_DATE;
                _design.DESIGN_ISDELETED = res.DESIGN_ISDELETED;
                _design.TS_CNT = res.TS_CNT;
                return _design;
            }
            catch (Exception ex)
            {
                Utilities objUtilities = new Utilities();
                objUtilities.LogError(ex);
                return null;
            }
        }

        public string UpdateDesign(pcms_design _Design)
        {

            string Msg = "";
            try
            {
                PCMS_DbContext context = new PCMS_DbContext();
                pcms_design nUser = context.pcms_design.Single(u => u.DESIGN_ID == _Design.DESIGN_ID);
                nUser.DESIGN_DESCRIPTION = Cryptographys.Encrypt(_Design.DESIGN_DESCRIPTION);
                nUser.DESIGN_HTML = Cryptographys.Encrypt(_Design.DESIGN_HTML);
                context.SaveChanges();
                Msg = "Updated";
                return Msg;
            }
            catch (Exception ex)
            {
                Utilities objUtilities = new Utilities();
                objUtilities.LogError(ex);
                Msg = "Something went wrong";
                return null;
            }
        }

        public List<pcms_popup> getpopUp()
        {
            try
            {
                using (PCMS_DbContext _pcmsDbContext = new PCMS_DbContext())
                {
                    var popupDesign = (_pcmsDbContext.pcms_popup
                        .Select(cam => new
                        {
                            POPUP_ID = cam.POPUP_ID,
                            POPUP_NAME = cam.POPUP_NAME,
                            POPUP_DESCRIPTION = cam.POPUP_DESCRIPTION,
                        }).ToList()
                              .Select(x => new pcms_popup()
                              {
                                  POPUP_ID = x.POPUP_ID,
                                  POPUP_NAME = Cryptographys.Decrypt(x.POPUP_NAME),
                                  POPUP_DESCRIPTION = Cryptographys.Decrypt(x.POPUP_DESCRIPTION)
                              }).OrderByDescending(y => y.INSERTED_DATE)).ToList();
                    if (popupDesign.Count > 0)
                        return popupDesign;
                    else
                        return new List<pcms_popup>();
                }
            }
            catch (Exception ex)
            {
                Utilities objUtilities = new Utilities();
                objUtilities.LogError(ex);
                return null;
            }
        }

        public string AddpopUp(pcms_popup _Popup)
        {
            string Msg = "";
            try
            {
                PCMS_DbContext context = new PCMS_DbContext();
                if (!context.pcms_popup.Any(u => u.POPUP_NAME == _Popup.POPUP_NAME))
                {
                    pcms_popup pu = new pcms_popup();
                    pu.POPUP_PK = Guid.NewGuid();
                    pu.POPUP_NAME = Cryptographys.Encrypt(_Popup.POPUP_NAME);
                    pu.POPUP_DESCRIPTION = Cryptographys.Encrypt(_Popup.POPUP_DESCRIPTION);
                    pu.POPUP_HTML = Cryptographys.Encrypt(_Popup.POPUP_HTML);
                    pu.INSERTED_BY = _Popup.INSERTED_BY;
                    pu.INSERTED_DATE = _Popup.INSERTED_DATE;
                    pu.POPUP_ISDELETED = _Popup.POPUP_ISDELETED;
                    pu.TS_CNT = _Popup.TS_CNT;
                    context.AddTopcms_popup(pu);
                    context.SaveChanges();
                    Msg = "Inserted";
                }
                else { Msg = "Duplicate"; }
                return Msg;
            }
            catch (Exception ex)
            {
                Utilities objUtilities = new Utilities();
                objUtilities.LogError(ex);
                Msg = "Something went wrong";
                return null;
            }
        }

        public string DeletepopUp(int query)
        {
            string Msg = "";
            try
            {
                PCMS_DbContext context = new PCMS_DbContext();
                pcms_popup poupu = context.pcms_popup.Single(x => x.POPUP_ID == query);
                context.pcms_popup.DeleteObject(poupu);
                context.SaveChanges();
                Msg = "Deleted";
                return Msg;
            }
            catch (Exception ex)
            {
                Utilities objUtilities = new Utilities();
                objUtilities.LogError(ex);
                Msg = "Something went wrong";
                return null;
            }
        }

        public pcms_popup EditpopUp(int query)
        {
            try
            {
                pcms_popup _popup = new pcms_popup();
                PCMS_DbContext context = new PCMS_DbContext();
                var res = context.pcms_popup.Single(popup => popup.POPUP_ID == query);
                pcms_design dn = new pcms_design();
                _popup.POPUP_ID = res.POPUP_ID;
                _popup.POPUP_NAME = Cryptographys.Decrypt(res.POPUP_NAME);
                _popup.POPUP_DESCRIPTION = Cryptographys.Decrypt(res.POPUP_DESCRIPTION);
                _popup.POPUP_HTML = Cryptographys.Decrypt(res.POPUP_HTML);
                _popup.INSERTED_BY = res.INSERTED_BY;
                _popup.INSERTED_DATE = res.INSERTED_DATE;
                _popup.POPUP_ISDELETED = res.POPUP_ISDELETED;
                _popup.TS_CNT = res.TS_CNT;
                return _popup;
            }
            catch (Exception ex)
            {
                Utilities objUtilities = new Utilities();
                objUtilities.LogError(ex);
                return null;
            }
        }

        public string UpdatepopUp(pcms_popup _Popup)
        {
            string Msg = "";
            try
            {
                PCMS_DbContext context = new PCMS_DbContext();
                pcms_popup nUser = context.pcms_popup.Single(u => u.POPUP_ID == _Popup.POPUP_ID);
                nUser.POPUP_DESCRIPTION = Cryptographys.Encrypt(_Popup.POPUP_DESCRIPTION);
                nUser.POPUP_HTML = Cryptographys.Encrypt(_Popup.POPUP_HTML);
                context.SaveChanges();
                Msg = "Updated";
                return Msg;
            }
            catch (Exception ex)
            {
                Utilities objUtilities = new Utilities();
                objUtilities.LogError(ex);
                Msg = "Something went wrong";
                return null;
            }
        }

        public List<pcms_rules> getURLBYID(string query)
        {

            try
            {
                using (PCMS_DbContext _pcmsDbContext = new PCMS_DbContext())
                {
                    var rules = (_pcmsDbContext.pcms_rules
                        .Select(cam => new
                        {
                            RULE_ID = cam.RULE_ID,
                            RULE_APPLIED_URL = cam.RULE_APPLIED_URL,
                            WEBSITE_URL = cam.WEBSITE_URL,
                        }).ToList()
                              .Select(x => new pcms_rules()
                              {
                                  RULE_ID = x.RULE_ID,
                                  RULE_APPLIED_URL = Cryptographys.Decrypt(x.RULE_APPLIED_URL),
                                  WEBSITE_URL = Cryptographys.Decrypt(x.WEBSITE_URL)
                              }).Where(y => y.RULE_ID == Convert.ToInt16(query)).OrderByDescending(y => y.INSERTED_DATE)).ToList();
                    if (rules.Count > 0)
                        return rules;
                    else
                        return new List<pcms_rules>();
                }
            }
            catch (Exception ex)
            {
                Utilities objUtilities = new Utilities();
                objUtilities.LogError(ex);
                return null;
            }
        }

        public List<pcms_country_list> GetCountryList()
        {
            try
            {
                using (PCMS_DbContext _pcmsDbContext = new PCMS_DbContext())
                {
                    var campaignlst = (_pcmsDbContext.pcms_country_list
                        .Select(cam => new
                        {
                            COUNTRY_ID = cam.COUNTRY_ID,
                            COUNTRY_NAME = cam.COUNTRY_NAME,
                            COUNTRY_CODE = cam.COUNTRY_CODE,
                        }).ToList()
                              .Select(x => new pcms_country_list()
                              {
                                  COUNTRY_ID = x.COUNTRY_ID,
                                  COUNTRY_NAME = x.COUNTRY_NAME,
                                  COUNTRY_CODE = x.COUNTRY_CODE,
                              }).OrderBy(y => y.COUNTRY_NAME)).ToList();

                    if (campaignlst.Count > 0)
                        return campaignlst;
                    else
                        return new List<pcms_country_list>();
                }
            }
            catch (Exception ex)
            {
                Utilities objUtilities = new Utilities();
                objUtilities.LogError(ex);
                return null;
            }

        }

        public string updateRules(pcms_rules Rule)
        {
            string Msg = "";
            try
            {
                PCMS_DbContext context = new PCMS_DbContext();
                if (!context.pcms_rules.Any(u => u.RULE_NAME == Rule.RULE_NAME))
                {
                    pcms_rules pu = new pcms_rules();
                    pu.RULE_NAME = Cryptographys.Encrypt(Rule.RULE_NAME);
                    pu.RULE_LOCATION = Rule.RULE_LOCATION;
                    pu.RULE_USER_TYPE = Rule.RULE_USER_TYPE;
                    pu.RULE_PK = Guid.NewGuid();
                    pu.RULE_PARAMETER_KEY_VALUE = Rule.RULE_PARAMETER_KEY_VALUE;
                    pu.RULE_APPLIED_URL = Cryptographys.Encrypt(Rule.RULE_APPLIED_URL);
                    pu.RULE_ISDELETED = Rule.RULE_ISDELETED;
                    pu.INSERTED_BY = Rule.INSERTED_BY;
                    pu.INSERTED_DATE = Rule.INSERTED_DATE;
                    pu.WEBSITE_URL = Cryptographys.Encrypt(Rule.WEBSITE_URL);
                    pu.TS_CNT = Rule.TS_CNT;
                    context.AddTopcms_rules(pu);
                    context.SaveChanges();
                    Msg = "Inserted";
                }
                else { Msg = "Duplicate"; }
                return Msg;
            }
            catch (Exception ex)
            {
                Utilities objUtilities = new Utilities();
                objUtilities.LogError(ex);
                Msg = "Something went wrong";
                return null;
            }
        }

        public List<pcms_campaign> getCampaignByid(string query)
        {

            try
            {
                using (PCMS_DbContext _pcmsDbContext = new PCMS_DbContext())
                {
                    var campaignList = (_pcmsDbContext.pcms_campaign
                        .Select(cam => new
                        {
                            CAMPAIGN_ID = cam.CAMPAIGN_ID,
                            CAMPAIGN_NAME = cam.CAMPAIGN_NAME,
                            CAMPAIGN_START_DATE = cam.CAMPAIGN_START_DATE,
                            CAMPAIGN_END_DATE = cam.CAMPAIGN_END_DATE,
                            CAMPAIGN_STATUS = cam.CAMPAIGN_STATUS,
                            CAMPAIGN_DESIGN_ID = cam.CAMPAIGN_DESIGN_ID,
                            CAMPAIGN_CONTENT_ID = cam.CAMPAIGN_CONTENT_ID,
                            CAMPAIGN_POPUP_STATUS = cam.CAMPAIGN_POPUP_STATUS,
                            CAMPAIGN_POPUP_ID = cam.CAMPAIGN_POPUP_ID,
                            CAMPAIGN_RULE_ID = cam.CAMPAIGN_RULE_ID,
                            CAMPAIGN_URL = cam.CAMPAIGN_URL,
                            CAMPAIGN_REDIRECT_URL = cam.CAMPAIGN_REDIRECT_URL,
                            CAMPAIGN_CONTAINER_ID = cam.CAMPAIGN_CONTAINER_ID,
                            CAMPAIGN_DESCRIPTION = cam.CAMPAIGN_DESCRIPTION

                        }).ToList()
                              .Select(x => new pcms_campaign()
                              {
                                  CAMPAIGN_ID = x.CAMPAIGN_ID,
                                  CAMPAIGN_NAME = x.CAMPAIGN_NAME,
                                  CAMPAIGN_START_DATE = x.CAMPAIGN_START_DATE,
                                  CAMPAIGN_END_DATE = x.CAMPAIGN_END_DATE,
                                  CAMPAIGN_STATUS = x.CAMPAIGN_STATUS,
                                  CAMPAIGN_DESIGN_ID = x.CAMPAIGN_DESIGN_ID,
                                  CAMPAIGN_CONTENT_ID = x.CAMPAIGN_CONTENT_ID,
                                  CAMPAIGN_POPUP_STATUS = x.CAMPAIGN_POPUP_STATUS,
                                  CAMPAIGN_POPUP_ID = x.CAMPAIGN_POPUP_ID,
                                  CAMPAIGN_RULE_ID = x.CAMPAIGN_RULE_ID,
                                  CAMPAIGN_URL = Cryptographys.Decrypt(x.CAMPAIGN_URL),
                                  CAMPAIGN_REDIRECT_URL = Cryptographys.Decrypt(x.CAMPAIGN_REDIRECT_URL),
                                  CAMPAIGN_CONTAINER_ID = x.CAMPAIGN_CONTAINER_ID,
                                  CAMPAIGN_DESCRIPTION = Cryptographys.Decrypt(x.CAMPAIGN_DESCRIPTION),
                              }).Where(y => y.CAMPAIGN_ID == Convert.ToInt16(query)).OrderByDescending(y => y.INSERTED_DATE)).ToList();
                    if (campaignList.Count > 0)
                        return campaignList;
                    else
                        return new List<pcms_campaign>();
                }
            }
            catch (Exception ex)
            {
                Utilities objUtilities = new Utilities();
                objUtilities.LogError(ex);
                return null;
            }
        }

        public string UpdateCamaign(pcms_campaign _campaign)
        {
            string Msg = "";
            try
            {
                using (PCMS_DbContext _pcmsDbContext = new PCMS_DbContext())
                {
                    //Perform Update operation
                    pcms_campaign cm = _pcmsDbContext.pcms_campaign.Single(u => u.CAMPAIGN_ID == _campaign.CAMPAIGN_ID);
                    cm.CAMPAIGN_NAME = _campaign.CAMPAIGN_NAME;
                    cm.CAMPAIGN_DESCRIPTION = Cryptographys.Encrypt(_campaign.CAMPAIGN_DESCRIPTION);
                    cm.CAMPAIGN_START_DATE = Convert.ToDateTime(_campaign.CAMPAIGN_START_DATE);
                    cm.CAMPAIGN_END_DATE = Convert.ToDateTime(_campaign.CAMPAIGN_END_DATE);
                    cm.CAMPAIGN_STATUS = _campaign.CAMPAIGN_STATUS;
                    cm.CAMPAIGN_DESIGN_ID = _campaign.CAMPAIGN_DESIGN_ID;
                    cm.CAMPAIGN_CONTENT_ID = _campaign.CAMPAIGN_CONTENT_ID;
                    cm.CAMPAIGN_POPUP_STATUS = _campaign.CAMPAIGN_POPUP_STATUS;
                    cm.CAMPAIGN_POPUP_ID = _campaign.CAMPAIGN_POPUP_ID;
                    cm.CAMPAIGN_RULE_ID = _campaign.CAMPAIGN_RULE_ID;
                    cm.CAMPAIGN_URL = Cryptographys.Encrypt(_campaign.CAMPAIGN_URL);
                    cm.CAMPAIGN_REDIRECT_URL = Cryptographys.Encrypt(_campaign.CAMPAIGN_REDIRECT_URL);
                    cm.CAMPAIGN_CONTAINER_ID = _campaign.CAMPAIGN_CONTAINER_ID;
                    cm.INSERTED_BY = _campaign.INSERTED_BY;
                    cm.INSERTED_DATE = _campaign.INSERTED_DATE;
                    cm.TS_CNT = _campaign.TS_CNT;
                    _pcmsDbContext.SaveChanges();
                    Msg = "Updated";
                    return Msg;
                }

            }
            catch (Exception ex)
            {
                Utilities objUtilities = new Utilities();
                objUtilities.LogError(ex);
                Msg = "Something went wrong";
                return null;
            }
        }

        public List<pcms_website> getWebURL()
        {
            string Msg = "";
            List<pcms_website> rs = new List<pcms_website>();
            try
            {
                using (PCMS_DbContext _pcmsDbContext = new PCMS_DbContext())
                {
                    var website = (_pcmsDbContext.pcms_website
                        .Select(cam => new
                        {
                            WEBSITE_ID = cam.WEBSITE_ID,
                            WEBSITE_URL = cam.WEBSITE_URL
                        }).ToList()
                              .Select(x => new pcms_website()
                              {
                                  WEBSITE_ID = x.WEBSITE_ID,
                                  WEBSITE_URL = x.WEBSITE_URL,
                              }).OrderByDescending(y => y.INSERTED_DATE)).ToList();

                    if (website.Count > 0)
                        return website;
                    else
                        return new List<pcms_website>();
                }
            }
            catch (Exception ex)
            {
                Utilities objUtilities = new Utilities();
                objUtilities.LogError(ex);
                Msg = "Something went wrong";
                return null;
            }
        }

        public string SaveURL(string usr, string url, string usrby)
        {
            string Msg = "";
            try
            {
                using (PCMS_DbContext _pcmsDbContext = new PCMS_DbContext())
                {
                    pcms_rules objRules = new pcms_rules();
                    objRules.WEBSITE_URL = Cryptographys.Encrypt(usr);
                    objRules.RULE_APPLIED_URL = Cryptographys.Encrypt(url);
                    objRules.INSERTED_BY = usrby;
                    objRules.INSERTED_DATE = DateTime.Now;
                    objRules.TS_CNT = 1;
                    objRules.RULE_PK = Guid.NewGuid();
                    _pcmsDbContext.AddTopcms_rules(objRules);
                    _pcmsDbContext.SaveChanges();
                    Msg = "Inserted";
                    return Msg;
                }
            }
            catch (Exception ex)
            {
                return Msg = ex.Message;
            }
        }
        public List<pcms_campaign> GetCampaignList(string query)
        {
            try
            {
                using (PCMS_DbContext _pcmsDbContext = new PCMS_DbContext())
                {
                    var campaignlst = (_pcmsDbContext.pcms_campaign
                        .Select(cam => new
                        {
                            PK_CAMPAIGN = cam.PK_CAMPAIGN,
                            CAMPAIGN_ID = cam.CAMPAIGN_ID,
                            CAMPAIGN_NAME = cam.CAMPAIGN_NAME,
                            CAMPAIGN_START_DATE = cam.CAMPAIGN_START_DATE,
                            CAMPAIGN_END_DATE = cam.CAMPAIGN_END_DATE,
                            CAMPAIGN_STATUS = cam.CAMPAIGN_STATUS
                        }).ToList()
                              .Select(x => new pcms_campaign()
                              {
                                  PK_CAMPAIGN = x.PK_CAMPAIGN,
                                  CAMPAIGN_ID = x.CAMPAIGN_ID,
                                  CAMPAIGN_NAME = x.CAMPAIGN_NAME,
                                  CAMPAIGN_START_DATE = x.CAMPAIGN_START_DATE,
                                  CAMPAIGN_END_DATE = x.CAMPAIGN_END_DATE,
                                  CAMPAIGN_STATUS = x.CAMPAIGN_STATUS
                              }).Where(y => y.CAMPAIGN_RULE_ID == Convert.ToInt16(query)).OrderByDescending(y => y.CAMPAIGN_START_DATE)).ToList();

                    if (campaignlst.Count > 0)
                        return campaignlst;
                    else
                        return new List<pcms_campaign>();
                }
            }
            catch (Exception ex)
            {
                Utilities objUtilities = new Utilities();
                objUtilities.LogError(ex);
                return null;
            }
        }
    }
}
