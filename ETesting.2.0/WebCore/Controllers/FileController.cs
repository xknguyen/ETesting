﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using WebCore.Models;

namespace WebCore.Controllers
{
    [Authorize]
    public class FileController : Controller
    {
        [HttpPost]
        public ActionResult GetDirectory()
        {
            var userId = User.Identity.GetUserId();
            //Lấy thư mục của tài khoản
            var path = Server.MapPath("~\\Upload\\" + userId);

            // Kiểm tra nếu chưa có thư mục thì tạo
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var data = new List<DataGroupModel>()
            {
                new DataGroupModel()
                {
                    Text = "Thư mục của tôi",
                    Href = userId,
                    Nodes = GetFolder(userId)
                }
            };
            return Json(JsonConvert.SerializeObject(data,
                Formatting.Indented,
                new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore }));
        }

        public List<DataGroupModel> GetFolder(string path)
        {
            var pathtemp = Server.MapPath("~\\Upload\\" + path);
            var curFolder = new DirectoryInfo(pathtemp);
            var childFolder = curFolder.GetDirectories();
            if (childFolder.Length <= 0) return null;
            var result = new List<DataGroupModel>();
            foreach (var fol in childFolder)
            {
                var f = new DataGroupModel()
                {
                    Text = fol.Name,
                    Href = path + "\\" + fol.Name
                };
                f.Nodes = GetFolder(f.Href);
                result.Add(f);
            }
            return result;
        }

        [HttpPost]
        public ActionResult GetFile(string path, string type)
        {
            var pathtemp = Server.MapPath("~\\Upload\\" + path);

            var picture = new List<string>()
            {
                ".JPEG",".TIFF",".PNG",".GIF",".JPG"
            };

            var video = new List<string>()
            {
                ".MP4",".WEBM",".OGG"
            };

            var data = new List<DataGroupModel>();
            List<string> typeList = null;
            if (!Directory.Exists(pathtemp))
                return Json(JsonConvert.SerializeObject(data,
                    Formatting.Indented,
                    new JsonSerializerSettings {DefaultValueHandling = DefaultValueHandling.Ignore}));
            switch (type)
            {
                case "picture":
                    typeList = picture;
                    break;
                case "video":
                    typeList = video;
                    break;
            }
            var curFolder = new DirectoryInfo(pathtemp);
            foreach (var file in curFolder.GetFiles())
            {

                var typeFile = file.Extension.ToUpper();
                var da = new DataGroupModel()
                {
                    Text = file.Name,
                    Href = "\\Upload\\" + path + "\\" + file.Name
                };



                if (typeList == null)
                {
                    data.Add(da);
                }
                else if (typeList.Contains(typeFile))
                {
                    data.Add(da);
                }

            }


            return Json(JsonConvert.SerializeObject(data,
                Formatting.Indented,
                new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore }));
        }

        [HttpPost]
        public ActionResult DeleteFile(string path)
        {
            var pathtemp = Server.MapPath(path);
            var userId = User.Identity.GetUserId();
            var paths = path.Split('\\');
            try
            {
                if (paths[2] == userId)
                {
                    if (System.IO.File.Exists(pathtemp))
                    {
                        System.IO.File.Delete(pathtemp);
                        return Json(new
                        {
                            Success = true
                        });
                    }
                    return Json(new
                    {
                        Success = false,
                        Message = "File này không còn tồn tại"
                    });
                }
                return Json(new
                {
                    Success = false,
                    Message = "Bạn không có quyền xóa file này!!!"
                });
            }
            catch
            {
                return Json(new
                {
                    Success = false,
                    Message = "Lỗi không xác định, vui lòng thử lại sau"
                });
            }
        }

        [HttpPost]
        public ActionResult CreateFolder(string path)
        {
            var pathtemp = Server.MapPath("~\\Upload\\" + path);
            var userId = User.Identity.GetUserId();
            var paths = path.Split('\\');
            try
            {
                if (paths[0] != userId)
                    return Json(new
                    {
                        Success = false,
                        Message = "Bạn không có quyền thêm vào thư mục này!!!"
                    });
                if (Directory.Exists(pathtemp))
                    return Json(new
                    {
                        Success = false,
                        Message = "Thư mục này đã tồn tại!"
                    });
                Directory.CreateDirectory(pathtemp);
                return Json(new
                {
                    Success = true
                });
            }
            catch
            {
                return Json(new
                {
                    Success = false,
                    Message = "Lỗi không xác định, vui lòng thử lại sau"
                });
            }
        }

        [HttpPost]
        public ActionResult DeleteFolder(string path)
        {
            var pathtemp = Server.MapPath("~\\Upload\\" + path);
            var userId = User.Identity.GetUserId();
            var paths = path.Split('\\');
            try
            {
                if (paths[0] == userId)
                {
                    if (!Directory.Exists(pathtemp))
                        return Json(new
                        {
                            Success = false,
                            Message = "Thư mục này không còn tồn tại!"
                        });
                    Directory.Delete(pathtemp, true);
                    return Json(new
                    {
                        Success = true
                    });
                }
                return Json(new
                {
                    Success = false,
                    Message = "Bạn không có quyền xóa thư mục này!!!"
                });
            }
            catch
            {
                return Json(new
                {
                    Success = false,
                    Message = "Lỗi không xác định, vui lòng thử lại sau"
                });
            }
        }


        [HttpPost]
        public ActionResult Upload()
        {
            var mess = "";
            var result = true;
            if (Request.Files.Count > 0)
            {
                try
                {
                    var path = Request.Form["pictureFolder"];

                    var userId = User.Identity.GetUserId();
                    var paths = path.Split('\\');
                    if (paths[0] == userId)
                    {
                        var file = Request.Files[0];
                        if (file != null)
                        {
                            var pathtemp = Server.MapPath("~\\Upload\\" + path);
                            var uniqueFileName = DateTime.Now.Ticks + "_" + file.FileName;
                            file.SaveAs(pathtemp + "\\" + uniqueFileName);
                        }
                        else
                        {
                            result = false;
                            mess = "Bạn chưa chọn file nào!";
                        }

                    }
                    else
                    {
                        result = false;
                        mess = "Bạn không có quyền thêm vào thư mục này!!!";
                    }

                }
                catch (Exception ex)
                {
                    mess = ex.Message;
                    result = false;
                }

            }
            else
            {
                result = false;
                mess = "Bạn chưa chọn file nào!";
            }
            return Json(new { Success = result, Message = mess });
        }

    }
}