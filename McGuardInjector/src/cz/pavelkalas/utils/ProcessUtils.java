package cz.pavelkalas.utils;

import java.lang.management.ManagementFactory;

public class ProcessUtils {

	/*
	 * Gets and returns CURRENT process ID
	 * If failed, returns -1 as PID
	 */
	public static int currentProcessId() {
		String name = ManagementFactory.getRuntimeMXBean().getName();
		
		int pID;
		
		try {
			pID = Integer.parseInt(name.split("@")[0]);
		} catch (Exception e) {
			return -1;
		}
		
        return pID;
	}
}
