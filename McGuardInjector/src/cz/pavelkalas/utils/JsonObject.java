package cz.pavelkalas.utils;

import java.util.Map;

public class JsonObject {
	
	/**
	 * Converts a HashMap to JSON string and returns it.
	 * 
	 * @param HashMap list.
	 * @return JSON string.
	 */
	public static String convertToJson(Map<String, String> map) {
        StringBuilder jsonBuilder = new StringBuilder();
        
        jsonBuilder.append("{");

        for (Map.Entry<String, String> entry : map.entrySet()) {
            jsonBuilder.append("\"").append(escapeJson(entry.getKey())).append("\": \"").append(escapeJson(entry.getValue())).append("\", ");
        }

        if (jsonBuilder.length() > 1) {
            jsonBuilder.setLength(jsonBuilder.length() - 2);
        }

        jsonBuilder.append("}");

        return jsonBuilder.toString();
    }
	
	/**
     * Escapes special characters for JSON.
     *
     * @param value The string to escape.
     * @return The escaped string.
     */
    private static String escapeJson(String value) {
        if (value == null) {
            return "";
        }
        
        return value.replace("\\", "\\\\").replace("\"", "\\\"").replace("\b", "\\b").replace("\f", "\\f").replace("\n", "\\n").replace("\r", "\\r").replace("\t", "\\t");
    }
}
