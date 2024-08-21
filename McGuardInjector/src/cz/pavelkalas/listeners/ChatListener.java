package cz.pavelkalas.listeners;

import java.util.HashMap;

import org.bukkit.Location;
import org.bukkit.entity.Player;
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
		Player player = (Player) playerEvent.getPlayer();
		Location location = player.getLocation();
        
		// action
		data.put("action", "handle_chat");
		
		// data of action
		data.put("player_name", player.getName());
        data.put("player_has_op", "" + player.isOp());
        data.put("player_message", playerEvent.getMessage());
        data.put("player_flying", "" + player.isFlying());
        data.put("coords_x", "" + location.getX());
        data.put("coords_y", "" + location.getY());
        data.put("coords_z", "" + location.getZ());
        data.put("player_id", "" + player.getEntityId());
		
		ipc.sendToPipe(JsonObject.convertToJson(data));
	}
}
