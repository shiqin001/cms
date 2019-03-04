﻿using System;
using System.Collections.Specialized;
using System.Web.UI.WebControls;
using SiteServer.CMS.Caches;
using SiteServer.CMS.Database.Attributes;
using SiteServer.Utils;
using SiteServer.CMS.Database.Core;
using SiteServer.CMS.Database.Repositories.Contents;

namespace SiteServer.BackgroundPages.Cms
{
    public class ModalContentTidyUp : BasePageCms
    {
        public DropDownList DdlAttributeName;
        public DropDownList DdlIsDesc;
        
        private string _returnUrl;

        public static string GetOpenWindowString(int siteId, int channelId, string returnUrl)
        {
            return LayerUtils.GetOpenScriptWithCheckBoxValue("整理排序", PageUtils.GetCmsUrl(siteId, nameof(ModalContentTidyUp), new NameValueCollection
            {
                {"channelId", channelId.ToString()},
                {"ReturnUrl", StringUtils.ValueToUrl(returnUrl)}
            }), "contentIdCollection", "", 460, 320);
        }

        public void Page_Load(object sender, EventArgs e)
        {
            if (IsForbidden) return;

            _returnUrl = StringUtils.ValueFromUrl(AuthRequest.GetQueryString("ReturnUrl")).Replace("&DateFrom=&SearchType=&Keyword=&taxisByAddDate=", "");
            if (IsPostBack) return;

            var listItem = new ListItem("内容Id", ContentAttribute.Id) {Selected = true};
            DdlAttributeName.Items.Add(listItem);
            listItem = new ListItem("添加日期", ContentAttribute.AddDate);
            DdlAttributeName.Items.Add(listItem);

            listItem = new ListItem("正序", false.ToString());
            DdlIsDesc.Items.Add(listItem);
            listItem = new ListItem("倒序", true.ToString()) {Selected = true};
            DdlIsDesc.Items.Add(listItem);
        }

        public override void Submit_OnClick(object sender, EventArgs e)
        {
            var channelId = AuthRequest.GetQueryInt("channelId");
            var channelInfo = ChannelManager.GetChannelInfo(SiteId, channelId);

            channelInfo.ContentRepository.UpdateArrangeTaxis(channelId, DdlAttributeName.SelectedValue, TranslateUtils.ToBool(DdlIsDesc.SelectedValue));

            LayerUtils.CloseAndRedirect(Page, _returnUrl);
        }
    }
}
