static class MapParser {
    public static List<IRegion> Parse(string mapPath) {
        List<IRegion> map = new List<IRegion>();

        string mapText = System.IO.File.ReadAllText(mapPath);
        string[] rawRegions = mapText.Split("\\n");

        foreach(string r in rawRegions) {
            string rawRegion = String.Concat(r.Where(c => !Char.IsWhiteSpace(c)));

            if (rawRegion.Length < 16) {
                throw new ArgumentException($"invalid map line: \"{rawRegion}\"");
            }

            map.Add(parseRegion(rawRegion));
        }

        return map;
    }


    private static IRegion parseRegion(string rawRegion){
        string zone = parseZone(rawRegion);
        string shape = parseShape(rawRegion);
        (int,int) position = parsePosition(rawRegion);

        if (shape == "rectangle") {
            (int,int) size = parseRectangleSize(rawRegion);
            return new RectangleRegion(zone, position, size);
        }
        else {
            int radius = parseRadius(rawRegion);
            return new CircleRegion(zone, position, radius);
        }

    }

    private static string parseZone(string line) {
        string zone = line.Substring(0, 4);
        if (zone != "safe" && zone != "warn" && zone != "fire"){
            throw new ArgumentException($"invalid zone: {zone}");
        }
        return zone;
    }
    private static string parseShape(string line) {
        int zoneLength = 4;
        int index = line.IndexOf("-");

        if (index < 6) throw new ArgumentException($"invalid map line: \"{line}\"");

        string shape = line.Substring(zoneLength, index - zoneLength);

        if (shape != "circle" && shape != "rectangle"){
            throw new ArgumentException($"invalid shape: {shape}");
        }
    
        return shape;
    }
    private static (int,int) parsePosition(string line) {
        int start = line.IndexOf("(")+1;
        int end = line.IndexOf(")");

        if(start < 10 || end < 14) throw new ArgumentException($"invalid map line: \"{line}\"");

        string strPos = line.Substring(start, end-start);
        string[] strPosArr = strPos.Split(",");
        return (Int32.Parse(strPosArr[0]),Int32.Parse(strPosArr[1]));
    }
    private static (int,int) parseRectangleSize(string line) {
        int start = line.LastIndexOf("(")+1;
        int end = line.LastIndexOf(")");

        if(start < 15 || end < 19) throw new ArgumentException($"invalid map line: \"{line}\"");

        string strPos = line.Substring(start, end-start);
        string[] strPosArr = strPos.Split(",");
        return (Int32.Parse(strPosArr[0]),Int32.Parse(strPosArr[1]));
    }
    private static int parseRadius(string line) {
        int start = line.LastIndexOf(")")+1;
        return Int32.Parse(line.Substring(start));
    }
}