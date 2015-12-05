using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnliStam.Dashboard.Enums
{
    public enum MySqlTipovi
    {
        [Description("bigint")]
        _bigint,
        [Description("bit")]
        _bit,
        [Description("blob")]
        _blob,
        [Description("char")]
        _char,
        [Description("datetime")]
        _datetime,
        [Description("decimal")]
        _decimal,
        [Description("double")]
        _double,
        [Description("enum")]
        _enum,
        [Description("float")]
        _float,
        [Description("int")]
        _int,
        [Description("longblob")]
        _longblob,
        [Description("longtext")]
        _longtext,
        [Description("mediumblob")]
        _mediumblob,
        [Description("mediumtext")]
        _mediumtext,
        [Description("set")]
        _set,
        [Description("smallint")]
        _smallint,
        [Description("text")]
        _text,
        [Description("time")]
        _time,
        [Description("timestamp")]
        _timestamp,
        [Description("tinyint")]
        _tinyint,
        [Description("varchar")]
        _varchar
    }
}
