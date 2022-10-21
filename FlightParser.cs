static class FlightParser {
    public static List<Flight> Parse(string[] rawFlights) {
        List<Flight> flights = new List<Flight>();

        foreach(string r in rawFlights) {
            if (String.IsNullOrWhiteSpace(r)) {}
            else if (!validateFlight(r)) {
                Console.WriteLine($"{r} is not a valid flight!");
            }
            else {
                flights.Add(parseFlight(r));
            }
        }

        return flights;
    }

    private static Flight parseFlight(string rawFlight) {
        string code = parseFlightcode(rawFlight);
        (int,int) position = parsePosition(rawFlight);
        return new Flight(code, position);
    }
    private static bool validateFlight(string rawFlight) {
        if (!isUppercaseChar(rawFlight.Substring(0,2))) return false;
        
        int lengthOfDigits = rawFlight.IndexOf("(") - 2;
        if (!isDigitsOnly(rawFlight.Substring(2, lengthOfDigits))) return false;
        int positionStart = rawFlight.IndexOf("(")+1;
        int positionEnd = rawFlight.IndexOf(")") - positionStart;
        if (!isValidPosition(rawFlight.Substring(positionStart, positionEnd))) return false;
        return true;
    }
    private static (int,int) parsePosition(string rawFlight) {
        int start = rawFlight.IndexOf("(") + 1;
        int end = rawFlight.IndexOf(")") - start;
        string[] strPosition = rawFlight.Substring(start, end).Split(",");
        
        return (Int32.Parse(strPosition[0]),Int32.Parse(strPosition[1]));
    }
    private static string parseFlightcode(string rawFlight) {
        int end = rawFlight.IndexOf("(");
        return rawFlight.Substring(0,end);
    }
    private static bool isUppercaseChar(string text) {
        foreach (char c in text) {
            if (c < 'A' || c > 'Z') {
                return false;
            }
        }
        return true;
    }
    private static bool isDigitsOnly(string digits) {
        foreach (char c in digits) {
            if (c < '0' || c > '9') {
                return false;
            }
        }
        return true;
    }
    private static bool isValidPosition(string position) {
        string[] strPos = position.Split(",");
        if (strPos.Length != 2) return false;
        if(!isDigitsOnly(strPos[0]) && !isDigitsOnly(strPos[1])) return false;
        return true;
    }
}