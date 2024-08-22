package cz.pavelkalas.listeners;

import java.util.HashMap;

import org.bukkit.Location;
import org.bukkit.entity.Player;
import org.bukkit.event.EventHandler;
import org.bukkit.event.Listener;
import org.bukkit.event.player.PlayerJoinEvent;

import cz.pavelkalas.utils.IPC;
import cz.pavelkalas.utils.JsonObject;

public class ConnectionListener implements Listener {
	
	private IPC ipc;
	
	public ConnectionListener(IPC ipc) {
		this.ipc = ipc;
	}
	
	@EventHandler
	public void onPlayerJoin(PlayerJoinEvent playerEvent) {
		playerEvent.setJoinMessage(null);
		Player player = playerEvent.getPlayer();
		
		HashMap<String, String> data = new HashMap<>();
		Location location = player.getLocation();
        
		// action
		data.put("action", "handle_playerjoin");
		
		// data of action
		data.put("player_name", player.getName());
        data.put("player_has_op", "" + player.isOp());
        data.put("player_flying", "" + player.isFlying());
        data.put("coords_x", "" + location.getX());
        data.put("coords_y", "" + location.getY());
        data.put("coords_z", "" + location.getZ());
        data.put("player_id", "" + player.getEntityId());
        data.put("player_ip", "" + player.getAddress().getAddress());
        
        
        ipc.sendToPipe(JsonObject.convertToJson(data));
	}
}
