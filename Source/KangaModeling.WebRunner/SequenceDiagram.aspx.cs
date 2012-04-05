﻿using System;
using System.Drawing;
using System.Drawing.Imaging;
using KangaModeling.Facade;

namespace KangaModelling.WebApplication
{
	public class SequenceDiagram : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			string codeText = Request["code"] ?? string.Empty;

			var arguments = new DiagramArguments(codeText, DiagramType.Sequence, DiagramStyle.Sketchy);
			var result = DiagramFactory.Create(arguments);
			using (result)
			{
				Response.Cache.SetCacheability(System.Web.HttpCacheability.Public);
				Response.ContentType = "image/png";
				result.Image.Save(Response.OutputStream, ImageFormat.Png);
			}
		}
	}
}
