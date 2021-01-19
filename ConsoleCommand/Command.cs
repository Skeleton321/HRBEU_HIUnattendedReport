using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using static ConsoleCommand.Misc;

namespace ConsoleCommand
{
    public class Command
    {
        private static readonly Command instance = new Command();
        private Command() { }

        public static Command CommandFactory()
        {
            return instance;
        }

        public static T DeserializeObject<T>(string[] args)
        {
            T t = CreateInstance<T>();
            Type type = t.GetType();
            var properties = type.GetProperties();
            Dictionary<string, int> mainKey = new Dictionary<string, int>();
            Dictionary<string, int> aliasKey = new Dictionary<string, int>();

            for (int i = 0; i < properties.Length; i++)
            {
                ref PropertyInfo property = ref properties[i];

                mainKey.Add($"--{property.Name}", i);

                if (!property.IsDefined(typeof(Alias), false)) continue;
                var attributes = properties[i].GetCustomAttribute<Alias>();

                string alias = attributes.Val;

                aliasKey.Add($"-{alias}", i);
            }

            for(int i = 0; i < args.Length; i++)
            {
                ref string arg = ref args[i];
                if (string.IsNullOrWhiteSpace(arg)) continue;
                if (arg[0] != '-') continue;
                PropertyInfo property = null;
                object value = null;
                try
                {
                    if (arg[1] == '-')
                        property = properties[mainKey[arg]];
                    else
                        property = properties[aliasKey[arg]];

                    if (property.PropertyType == typeof(bool))
                        value = true;
                    else
                    {
                        if (i + 1 >= args.Length)
                            throw new IllegalArgumentException($"第{i + 1}个参数({arg})用法错误。请使用--help查看正确用法。");

                        arg = ref args[++i];
                        if (arg[0] == '-')
                            throw new IllegalArgumentException($"第{i + 1}个参数({arg})用法错误。请使用--help查看正确用法。");
                        else
                            value = arg.Trim().Trim(new[] { '"' });
                    }

                    Type ptype = property.PropertyType;

                    try
                    {
                        if (ptype == typeof(long))
                            property.SetValue(t, Convert.ToInt64(value));
                        else if (ptype == typeof(int))
                            property.SetValue(t, Convert.ToInt32(value));
                        else
                            property.SetValue(t, value);
                    }catch(Exception e)
                    {
                        Console.WriteLine($"第{i + 1}个参数({arg})类型不受支持。\n{e.Message}");
                    }
                }
                catch (KeyNotFoundException)
                {
                    Console.WriteLine($"第{i + 1}个参数非法。参数表中无该参数。");
                }
            }

            for (int i = 0; i < properties.Length; i++)
            {
                ref PropertyInfo property = ref properties[i];

                if (property.IsDefined(typeof(Necessary)))
                {
                    object val = property.GetValue(t);
                    if (val == null || val == default)
                        throw new MissNecessaryParameterException($"必需参数缺失: {property.Name}");
                }
            }

            return t;
        }
        
        public static void PrintHelpInfo<T>()
        {
            var properties = typeof(T).GetProperties();
            StringBuilder sb = new StringBuilder();
            sb.Append("用法:\n");
            foreach(var property in properties)
            {
                sb.Append("  ");
                sb.Append($"--{property.Name}");
                if(property.IsDefined(typeof(Alias), false))
                {
                    string alias = property.GetCustomAttribute<Alias>().Val;
                    sb.Append($", -{alias}");
                }
                if(property.PropertyType != typeof(bool))
                    sb.Append($" <{property.Name}>");
                sb.Append("  \t");

                if (property.IsDefined(typeof(Necessary), false))
                    sb.Append("必需参数");
                else
                    sb.Append("可选参数");
                sb.Append("  \t");

                if(property.IsDefined(typeof(Description), false))
                {
                    string descriptoin = property.GetCustomAttribute<Description>().Val;
                    sb.Append(descriptoin);
                }
                sb.Append("\n");
            }

            Console.WriteLine(sb.ToString());
        }
    }
}
