using Asset.Web.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OurHr.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Asset.Web.Services
{
    public class ReadDir
    {
        [Obsolete]
        private readonly IHostingEnvironment _hostingEnvironment;


        [Obsolete]
        public ReadDir(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        [Obsolete]
        public List<DirModel> ReadMyDir(string path)
        {

            // string folderPath = _hostingEnvironment.ContentRootPath + "\\controllers\\";
            string folderPath = _hostingEnvironment.ContentRootPath;

            var realPath = folderPath + path;
            List<DirModel> dirListModel = MapDirsKontroller(realPath);
            return dirListModel;
        }

        public List<FileModel> ReadfileDir(string path)
        {

            string folderPath = _hostingEnvironment.ContentRootPath + "\\controllers\\";
            var realPath = folderPath + path;
            List<FileModel> dirListModel = MapFiles(realPath, path);
            return dirListModel;
        }

        private List<DirModel> MapDirsKontroller(String realPath)
        {
            List<DirModel> dirListModel = new List<DirModel>();

            // IEnumerable<string> dirList = Directory.EnumerateDirectories(realPath);
            IEnumerable<string> dirList = Directory.EnumerateFiles(realPath);
            foreach (string file in dirList)
            {
                DirModel dirModel = new DirModel();
                FileInfo f = new FileInfo(file);
                if (f.Extension.ToLower() != "php" && f.Extension.ToLower() != "aspx"
                   && f.Extension.ToLower() != "asp" && f.Extension.ToLower() != "exe" && f.Extension != "cshtml" && f.Extension != "cs")
                {


                    dirModel.DirName = Path.GetFileName(file);
                    dirModel.DirName = dirModel.DirName.Substring(0, dirModel.DirName.IndexOf("."));
                    dirModel.DirName = dirModel.DirName.Remove(dirModel.DirName.IndexOf("Controller"));

                    // DirAccessed = d.LastAccessTime

                }



                dirListModel.Add(dirModel);
            }

            return dirListModel;
        }

        private List<DirModel> MapDirs(String realPath)
        {
            List<DirModel> dirListModel = new List<DirModel>();

            // IEnumerable<string> dirList = Directory.EnumerateDirectories(realPath);
            IEnumerable<string> dirList = Directory.EnumerateDirectories(realPath);
            foreach (string dir in dirList)
            {
                DirectoryInfo d = new DirectoryInfo(dir);

                DirModel dirModel = new DirModel
                {
                    DirName = Path.GetFileName(dir),
                    DirAccessed = d.LastAccessTime
                };

                dirListModel.Add(dirModel);
            }

            return dirListModel;
        }

        private List<FileModel> MapFiles(String realPath)
        {
            List<FileModel> fileListModel = new List<FileModel>();

            IEnumerable<string> fileList = Directory.EnumerateFiles(realPath);
            foreach (string file in fileList)
            {
                FileInfo f = new FileInfo(file);

                FileModel fileModel = new FileModel();

                if (f.Extension.ToLower() != "php" && f.Extension.ToLower() != "aspx"
                    && f.Extension.ToLower() != "asp" && f.Extension.ToLower() != "exe" && f.Extension != "cshtml" && f.Extension != "cs")
                {
                    fileModel.FileName = Path.GetFileName(file);
                    //fileModel.FileName = fileModel.FileName.Remove(fileModel.FileName.IndexOf("Controller"));

                    fileModel.MyMethod = fileModel.FileName.Substring(0, fileModel.FileName.IndexOf(".") + 1);

                    fileModel.MyKlass = realPath;
                    fileModel.FileAccessed = f.LastAccessTime;
                    fileModel.FileSizeText = (f.Length < 1024) ? f.Length.ToString() + " B" : f.Length / 1024 + " KB";

                    fileListModel.Add(fileModel);
                }
            }

            return fileListModel;
        }

        public List<FileModel> GetActionMethods(string KontrollerName)
        {
            var MyMethodList = ActionMethods(KontrollerName);
            return MyMethodList;
        }

        private List<FileModel> MapFiles(String realPath, string Methy)
        {
            List<FileModel> fileListModel = new List<FileModel>();

            IEnumerable<string> fileList = Directory.EnumerateFiles(realPath);
            foreach (string file in fileList)
            {
                FileInfo f = new FileInfo(file);

                FileModel fileModel = new FileModel();

                if (f.Extension.ToLower() != "php" && f.Extension.ToLower() != "aspx"
                    && f.Extension.ToLower() != "asp" && f.Extension.ToLower() != "exe" && f.Extension != "cshtml" && f.Extension != "cs")
                {
                    fileModel.FileName = Path.GetFileName(file);
                    fileModel.FileName = fileModel.FileName.Remove(fileModel.FileName.IndexOf("Controller"));
                    fileModel.MyMethod = fileModel.FileName.Substring(0, fileModel.FileName.IndexOf("."));
                    fileModel.MyKlass = Methy;
                    fileModel.FileAccessed = f.LastAccessTime;
                    fileModel.FileSizeText = (f.Length < 1024) ? f.Length.ToString() + " B" : f.Length / 1024 + " KB";

                    fileListModel.Add(fileModel);
                }
            }

            return fileListModel;
        }


          public List<FileModel> ActionMethods(string ControllerName)
        {
            ControllerName = ControllerName + "" + "Controller";
            Assembly asm = Assembly.GetExecutingAssembly();
            var controlleractionlist = asm.GetTypes()
                    .Where(type => typeof(Controller).IsAssignableFrom(type))
                    .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
                    .Select(x => new
                    {
                        Controller = x.DeclaringType.Name,
                        Action = x.Name,
                        Area = x.DeclaringType.CustomAttributes.Where(c => c.AttributeType == typeof(AreaAttribute))

                    }).ToList();
            var list = new List<FileModel>();

            var THeController = controlleractionlist.Where(k => k.Controller == ControllerName).ToList();
            foreach (var item in THeController)
            {
                if (item.Area.Count() != 0)
                {
                    list.Add(new FileModel()
                    {
                        MyKlass = ControllerName.Remove(ControllerName.IndexOf("Controller")),
                        MyMethod = item.Action,
                        // Area = item.Area.Select(v => v.ConstructorArguments[0].Value.ToString()).FirstOrDefault()
                    });
                }
                else
                {
                    list.Add(new FileModel()
                    {
                        MyKlass = ControllerName.Remove(ControllerName.IndexOf("Controller")),
                        MyMethod = item.Action,
                        // Area = null,
                    });
                }
            }
            return list;
        }
        public List<ControllerActions> GetActions(string ControllerName)
        {
            ControllerName = ControllerName ;
            Assembly asm = Assembly.GetExecutingAssembly();
            var controlleractionlist = asm.GetTypes()
                    .Where(type => typeof(Controller).IsAssignableFrom(type))
                    .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
                    .Select(x => new
                    {
                        Controller = x.DeclaringType.Name,
                        Action = x.Name,
                        Area = x.DeclaringType.CustomAttributes.Where(c => c.AttributeType == typeof(AreaAttribute))

                    }).ToList();
            var list = new List<ControllerActions>();

            var THeController = controlleractionlist.Where(k => k.Controller == ControllerName).ToList();
            foreach (var item in THeController)
            {
                if (item.Area.Count() != 0)
                {
                    list.Add(new ControllerActions()
                    {
                        Controller = ControllerName,
                        Action = item.Action,
                        // Area = item.Area.Select(v => v.ConstructorArguments[0].Value.ToString()).FirstOrDefault()
                    });
                }
                else
                {
                    list.Add(new ControllerActions()
                    {
                        Controller = ControllerName,// ControllerName.Remove(ControllerName.IndexOf("Controller")),
                        Action = item.Action,
                        // Area = null,
                    }) ;
                }
            }
            return list;
        }

        public List<ControllerActions> MyController()
         {
            Assembly asm = Assembly.GetExecutingAssembly();
            var controlleractionlist = asm.GetTypes()
                    .Where(type => typeof(Controller).IsAssignableFrom(type))
                    .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
                    .Select(x => new
                    {
                        Controller = x.DeclaringType.Name,
                        Action = x.Name,
                        Area = x.DeclaringType.CustomAttributes.Where(c => c.AttributeType == typeof(AreaAttribute))

                    }).ToList();
           var list = new List<ControllerActions>();
           var query = new List<ControllerActions>();
            foreach (var item in controlleractionlist)
            {
                if (item.Area.Count() != 0)
                {
                  
                    list.Add(new ControllerActions()
                    {
                        Controller = item.Controller,
                        Action = item.Action,
                        Area = item.Area.Select(v => v.ConstructorArguments[0].Value.ToString()).FirstOrDefault()
                    });
                }
                else
                {
                    if (list.Any(K => K.Controller == item.Controller))
                    {

                    }
                    else
                    {
                        list.Add(new ControllerActions()
                        {
                            Controller = item.Controller,
                            Action = item.Action,
                            Area = null,
                        });
                    }

                   
                }
            }
            // query = list.GroupBy(p => p.Controller).ToList();
           // MyList = list.Distinct().ToList();
            return list;
           
         }

    }
}