using System;
using System.Collections.Generic;

namespace MyCore.Model
{
    public class DataModel
    {
        public string Name { get; set; }

        public List<DataModel> TableList { get; set; }
    }

    public class TableModel
    {
        public string TableName { get; set; }

        public string FieldName { get; set; }

        public string Type { get; set; }

        public string Length { get; set; }

        public string DefaultValue { get; set; }

        public string FieldDescription { get; set; }
    }
}