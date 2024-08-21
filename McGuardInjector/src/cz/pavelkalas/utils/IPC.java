package cz.pavelkalas.utils;

import java.io.FileOutputStream;
import java.io.IOException;
import java.io.OutputStream;

public class IPC {

	/*
	 * Process ID as named pipe path
	 */
	private final int processId;
	
	public IPC(int processId) {
		this.processId = processId;
	}
	
	/*
	 * Sends a data string to pipe
	 */
	public void sendToPipe(String pipeMessage) {
		String pipeName = "\\\\.\\pipe\\" + processId;
        
		try (OutputStream outputStream = new FileOutputStream(pipeName)) {
            outputStream.write(pipeMessage.getBytes());
            outputStream.flush();
        } catch (IOException e) {
            e.printStackTrace();
        }
	}
}
