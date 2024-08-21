package cz.pavelkalas.listeners;

import java.util.HashMap;

import org.bukkit.event.EventHandler;
import org.bukkit.event.Listener;
import org.bukkit.event.player.AsyncPlayerChatEvent;

import cz.pavelkalas.utils.IPC;
import cz.pavelkalas.utils.JsonObject;

public class ChatListener implements Listener {
	
	private IPC ipc;
	
	public ChatListener(IPC ipc) {
		this.ipc = ipc;
	}

	@EventHandler
	public void onAsyncPlayerChatEvent(AsyncPlayerChatEvent playerEvent) {
		playerEvent.setCancelled(true);

		HashMap<String, String> data = new HashMap<>();
        
		data.put("player_name", playerEvent.getPlayer().getName());
        data.put("player_has_op", "" + playerEvent.getPlayer().isOp());
        data.put("player_message", playerEvent.getMessage());
		
		ipc.sendToPipe(JsonObject.convertToJson(data));
	}
}
