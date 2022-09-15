using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcProject.Services
{
    public static class Resources
    {
        public static string Culture { get; set; } = "uk-UA";
        public static string GetEmptyStringException(string? culture=null)
        {
            if (culture == null) culture = Culture;
            switch (culture)
            {
                case "uk-UA": return "Порожній рядок неприпустимий";
                case "en-US": return "Empty string not allowed";
            }
            throw new Exception("Unsupported culture");
        }
        public static string GetInvalidCharMessage(char c, string? culture = null)
        {
            culture ??= Culture;  // вместо if-присваивания
            return culture switch  // Новая версия switch return
            {
                "uk-UA" => $"Недозволенний символ '{c}'",
                "en-US" => $"Invalid char '{c}'",
                 _ => throw new Exception("Unsupported culture")
            };

        }
        public static string GetOnlyOne_N_Exception(string? culture = null)
        {
            culture??= Culture;
            return culture switch
            {
                "uk-UA" => "Недозволенний символ, тільки одна 'N'",
                "en-US" => "Invalid number, only one 'N'",
                _ => throw new Exception("Unsupported culture")
            };
        }
        public static string GetInvalidTypeException(int objNumber,string type, string? culture = null)
        {
            culture ??= Culture;
            return culture switch
            {
                "uk-UA" => $"obj{objNumber}: тип '{type}' не підтримується",
                "en-US" => $"obj{objNumber}: type '{type}' unsupported",
                _ => throw new Exception("Unsupported culture")
            };
        }
    }
}
