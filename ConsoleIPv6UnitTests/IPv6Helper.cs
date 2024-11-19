using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleIPv6UnitTests;

/// <summary>
/// Statische Helper-Klasse, die aktuell zwei Methoden
/// zum Umwandeln von IPv6 Adressen bietet
/// 
/// - Nicht-Hex-Ziffern werden aktuell nicht bemängelt
/// - Ein einziges 0-Hextett wird aktuell nicht durch :: ersetzt
/// </summary>
public static class IPv6Helper
{
    /// <summary>
    /// Kürzt die übergebene IPv6-Adresse
    /// Führende Nullen im Hextett weglassen, 
    /// Längste Folge (>1) von Hextetts mit 0 durch :: ersetzen
    /// </summary>
    /// <param name="address"></param>
    /// <returns></returns>
    public static string ShortenAddress(string address)
    {
        string prefix = GetPrefix(address);
        string[] parts = GetParts(address);
        ShortenPartsWithZeros(parts);
        string shortened = ShortenPartsToString(parts);
        shortened = shortened + prefix;
        return shortened;
    }

    /// <summary>
    /// IPv6-Adresse in voller Länge darstellen
    /// </summary>
    /// <param name="address"></param>
    /// <returns></returns>
    public static string ExpandAddress(string address)
    {
        string prefix = GetPrefix(address);
        string[] parts = GetParts(address);
        string expanded = PartsToString(parts);
        expanded = expanded + prefix;
        return expanded;
    }

    /// <summary>
    /// Gibt den "Prefix" der übergebenen IPv6-Adresse zurück
    /// </summary>
    /// <param name="address"></param>
    /// <returns></returns>
    private static string GetPrefix(string address)
    {
        string prefix = "";
        int nSlash = address.IndexOf('/');
        if (nSlash > 0)
        {
            prefix = address.Substring(nSlash);
            System.Diagnostics.Debug.WriteLine("prefix: " + prefix);
        }
        return prefix;
    }

    /// <summary>
    /// Gibt die Teile der übergebenen IPv6-Adresse zurück
    /// </summary>
    /// <param name="address"></param>
    /// <returns></returns>
    private static string[] GetParts(string address)
    {
        string[] parts = new string[8];

        System.Diagnostics.Debug.WriteLine("address:" + address);
        int nSlash = address.IndexOf('/');
        if (nSlash > 0)
        {
            address = address.Substring(0, nSlash);
            System.Diagnostics.Debug.WriteLine("address:" + address);
        }

        int nParts1 = 0;
        int nDoubleColon = address.IndexOf("::");
        if (nDoubleColon > 0)
        {
            string[] parts1 = address.Substring(0, nDoubleColon).Split(':');
            address = address.Substring(nDoubleColon + 2);
            parts1.CopyTo(parts, 0);
            nParts1 = parts1.Length;
        }

        string[] parts2 = address.Split(':', StringSplitOptions.RemoveEmptyEntries);
        int nParts2 = parts2.Length;

        for (int i = nParts1; i < (parts.Length - nParts2); i++)
        {
            parts[i] = "0000";
        }

        parts2.CopyTo(parts, parts.Length - nParts2);

        return parts;
    }

    private static void ShortenPartsWithZeros(string[] parts)
    {
        // shorten parts with zeroes
        for (int i = 0; i < parts.Length; i++)
        {
            string part = parts[i];
            if (part != null)
            {
                // check
                // if ((part == "0") || (part == "0000"))
                if ((part == "0") || (part == "00") || (part == "000") || (part == "0000"))
                    parts[i] = "0";
                else if (part.StartsWith("000"))
                    parts[i] = part.Substring(3);
                else if (part.StartsWith("00"))
                    parts[i] = part.Substring(2);
                else if (part.StartsWith("0"))
                    parts[i] = part.Substring(1);
            }
        }
    }

    private static string ShortenPartsToString(string[] parts)
    {
        int maxPos = -1;
        int maxLen = 0;

        FindMaxRowOfZeros(parts, ref maxPos, ref maxLen);

        string shortened = CompinePartsToString(parts, maxPos, maxLen);

        return shortened;
    }
    static string CompinePartsToString(string[] parts, int maxPos, int maxLen)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < parts.Length; i++)
        {
            // double colon
            if ((i == maxPos) && (maxLen > 1))
            {
                sb.Append(':');
                if (i == 0)
                    sb.Append(':');
            }

            // check
            // if ((i == maxPos) || ((i > maxPos) && (i < maxPos + maxLen)))
            if (((i == maxPos) && (maxLen > 1)) || ((i > maxPos) && (i < maxPos + maxLen)))
                ; // skip
            else
            {
                sb.Append(parts[i]);
                if (i < 7)
                    sb.Append(':');
            }
        }

        return sb.ToString();
    }

    static void FindMaxRowOfZeros(string[] parts, ref int maxPos, ref int maxLen)
    {
        int pos = -1;
        int len = 0;
        for (int i = 0; i < parts.Length; i++)
        {
            string part = parts[i];

            if (part != null)
            {
                if (part == "0")
                {
                    if (len == 0)
                        pos = i;
                    len++;
                }
                else
                {
                    if (len > maxLen)
                    {
                        maxLen = len;
                        maxPos = pos;
                    }
                    len = 0;
                    pos = -1;
                }
            }
        }
        if (len > maxLen)
        {
            maxLen = len;
            maxPos = pos;
        }
    }

    private static string PartsToString(string[] parts)
    {
        StringBuilder sb = new StringBuilder();
        foreach (string s in parts)
        {
            int repeat0 = 4 - s.Length;
            if (repeat0 > 0)
                sb.Append('0', repeat0);
            sb.Append(s);
            sb.Append(':');
        }
        sb.Remove(sb.Length - 1, 1);
        return sb.ToString();
    }

    static string PadWithZeros(string str)
    {
        switch (str.Length)
        {
            case 0: return "0000";
            case 1: return "000" + str;
            case 2: return "00" + str;
            case 3: return "0" + str;
            default: return str;
        }
    }
}

