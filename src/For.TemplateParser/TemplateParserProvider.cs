﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Remoting;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace For.TemplateParser
{
    public class TemplateParserProvider
    {
        private readonly Core _core;
        public TemplateParserProvider()
        {
            _core = new Core();
        }

        public TemplateParserProvider(ITemplateCache cache)
        {
            _core = new Core(cache);
        }
        /// <summary>
        /// 組合物件與範本
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="template"></param>
        /// <returns></returns>
        public string BuildTemplate<T>(T obj, string template)
        {
            var n = _core.BuildTemplate(typeof(T), template);
            var templateQue = new Queue<NodeModel>(n);
            var sb = new StringBuilder();
            while (templateQue.Count > 0)
            {
                var item = templateQue.Dequeue();
                sb.Append(item.Type == NodeType.String ? item.NodeStringValue : item.NodeDelegateValue(obj));
            }
            return sb.ToString();
        }

        /// <summary>
        /// 組合物件與範本
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public string BuildTemplate<T>(T obj, Func<T, string> func)
        {
            return func(obj);
        }
        /// <summary>
        /// 清除所有快取
        /// </summary>
        public void ClearCaches()
        {
            _core.ClearCache();
        }
    }
}