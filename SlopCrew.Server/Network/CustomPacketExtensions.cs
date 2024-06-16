using SlopCrew.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SlopCrew.Server;

public static class CustomPacketExtensions {
    public static Dictionary<string,string> GetTagsFromPacketID(string packetIdWithFlags) {
        var dict = new Dictionary<string, string>();
        var inTag = false;
        var tagName = "";
        var inTagValue = false;
        var tagValue = "";
        foreach(var c in packetIdWithFlags) {
            if (!inTag) {
                if (c == '<') {
                    inTag = true;
                    tagName = "";
                    tagValue = "";
                    continue;
                }
            }
            else {
                if (c == '=') {
                    inTagValue = true;
                    continue;
                }
                if (c == '>') {
                    inTagValue = false;
                    inTag = false;
                    dict[tagName] = tagValue;
                    continue;
                }
                if (inTagValue) {
                    tagValue += c;
                    continue;
                } else {
                    tagName += c;
                }
            }
        }
        return dict;
    }
}
