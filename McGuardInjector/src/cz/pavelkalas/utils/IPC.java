package cz.pavelkalas.utils;

import java.io.FileOutputStream;
import java.io.IOException;
import java.io.OutputStream;

public class IPC {

	/**
	 * Process ID as named pipe path
	 */
	private final String pipeName;
	
	public IPC(String pipeName) {
		this.pipeName = pipeName;
	}
	
	/**
	 * Sends a data string to pipe
	 */
	public void sendToPipe(String pipeMessage) {
		try (OutputStream outputStream = new FileOutputStream( "\\\\.\\pipe\\" + pipeName)) {
            outputStream.write(pipeMessage.getBytes());
            outputStream.flush();
        } catch (IOException e) {
            e.printStackTrace();
        }
	}
}
