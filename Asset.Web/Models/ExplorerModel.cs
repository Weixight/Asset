using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asset.Web.Models
{
    public class ExplorerModel
    {
        public List<DirModel> DirModelList;
        public List<FileModel> FileModelList;

        public String FolderName;
        public String ParentFolderName;
        public String URL;

        public ExplorerModel(List<DirModel> dirModelList, List<FileModel> fileModelList)
        {
            DirModelList = dirModelList;
            FileModelList = fileModelList;
        }
    }
}
