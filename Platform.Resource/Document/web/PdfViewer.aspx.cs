﻿using System;
using System.Linq;
using System.Web.Configuration;
using Homory.Model;

namespace Document.web
{
	public partial class DocumentWebPdfViewer : HomoryResourcePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected string Path
		{
			get
			{
				try
				{
					var id = Request.QueryString["Id"];
					var key = Guid.Parse(id);
					var resource = HomoryContext.Value.Resource.First(o => o.Id == key).Preview;
					var url = string.Format("{0}{1}", WebConfigurationManager.AppSettings["ResourceUrl"], resource.Substring((3)));
					return url;
				}
				catch (Exception)
				{
					return string.Empty;
				}
			}
		}

		protected override bool ShouldOnline
		{
			get { return false; }
		}
	}
}