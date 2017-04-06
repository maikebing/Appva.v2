/*
using Appva.Cqrs;
using Appva.Mcss.Admin.Application.Services.Settings;
using Appva.Mcss.Admin.Areas.Backoffice.JsonObjects.Pdf;
using Appva.Mcss.Admin.Areas.Backoffice.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appva.Mcss.Admin.Areas.Backoffice.Models.Handlers
{
    public class ListGeneralSettingsPublisher : RequestHandler<ListGeneralSettingsModel, bool>
    {
        #region Properties.
        ISettingsService settingsService;


        #endregion

        private static String RGBConverter(System.Drawing.Color c)
        {
            return c.R.ToString() + "," + c.G.ToString() + "," + c.B.ToString();
        }

        #region Constructor.
        public ListGeneralSettingsPublisher(ISettingsService settingsService)
        {
            this.settingsService = settingsService;
        }
        #endregion

        public override bool Handle(ListGeneralSettingsModel message)
        {
            var settings = this.settingsService.List();
            

            if (message.List != null)
            {
                foreach (var item in message.List)
                {
                    if (item.IntValue == null && item.StringValue == null)
                    {
                        var setting = settings.Where(x => x.Id == item.Id).SingleOrDefault();


                        if (setting.Value != Convert.ToString(item.BoolValue))
                        {
                            setting.Update(setting.MachineName, setting.Namespace, setting.Name, setting.Description, Convert.ToString(item.BoolValue));
                        }


                    }

                    if (item.IntValue != null)
                    {
                        var setting = settings.Where(x => x.Id == item.Id).SingleOrDefault();

                        if (setting.Value != Convert.ToString(item.IntValue))
                        {
                            setting.Update(setting.MachineName, setting.Namespace, setting.Name, setting.Description, Convert.ToString(item.IntValue));
                        }
                    }

                    if (item.StringValue != null)
                    {
                        var setting = settings.Where(x => x.Id == item.Id).SingleOrDefault();

                        if (setting.Value != Convert.ToString(item.BoolValue))
                        {
                            var backgroundcolor = System.Drawing.ColorTranslator.FromHtml(message.Colors.BackgroundColor);
                            var fontcolor = System.Drawing.ColorTranslator.FromHtml(message.Colors.FontColor);
                            var tablebordercolor = System.Drawing.ColorTranslator.FromHtml(message.Colors.TableBorderColor);
                            var tableheadercolor = System.Drawing.ColorTranslator.FromHtml(message.Colors.TableHeaderColor);
                            string[] backgroundRGB = RGBConverter(backgroundcolor).Split(',');
                            string[] fontRGB = RGBConverter(fontcolor).Split(',');
                            string[] tableborderRGB = RGBConverter(tablebordercolor).Split(',');
                            string[] tableheaderRGB = RGBConverter(tableheadercolor).Split(',');

                            var _backgroundColor = new BackgroundColor
                            {
                                R = Convert.ToInt32(backgroundRGB[0]),
                                G = Convert.ToInt32(backgroundRGB[1]),
                                B = Convert.ToInt32(backgroundRGB[2]),
                            };

                            var _fontColor = new FontColor
                            {
                                R = Convert.ToInt32(fontRGB[0]),
                                G = Convert.ToInt32(fontRGB[1]),
                                B = Convert.ToInt32(fontRGB[2])
                            };

                            var _tableBorderColor = new TableBorderColor
                            {
                                R = Convert.ToInt32(tableborderRGB[0]),
                                G = Convert.ToInt32(tableborderRGB[1]),
                                B = Convert.ToInt32(tableborderRGB[2])
                            };

                            var _tableHeaderColor = new TableHeaderColor
                            {
                                R = Convert.ToInt32(tableheaderRGB[0]),
                                G = Convert.ToInt32(tableheaderRGB[1]),
                                B = Convert.ToInt32(tableheaderRGB[2])
                            };

                            var _pfdGenObject = new PdfGenObject
                            {
                                BackgroundColor = _backgroundColor,
                                FontColor = _fontColor,
                                TableBorderColor = _tableBorderColor,
                                TableHeaderColor = _tableHeaderColor
                            };

                            string pdfGenOnbject = JsonConvert.SerializeObject(_pfdGenObject);




                            setting.Update(setting.MachineName, setting.Namespace, setting.Name, setting.Description, item.StringValue.ToString());
                        }
                    }

                }

                    return true;
            }
            else
            {
                return false;
            }
            }
        }
    }
    */