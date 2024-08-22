package cz.pavelkalas.core;

import org.bukkit.Bukkit;
import org.bukkit.plugin.java.JavaPlugin;

import cz.pavelkalas.listeners.ChatListener;
import cz.pavelkalas.listeners.ConnectionListener;
import cz.pavelkalas.utils.IPC;

public class Startup extends JavaPlugin {
	
	@Override
	public void onEnable() {
		IPC ipc = new IPC("" + Bukkit.getPort());

		this.getServer().getPluginManager().registerEvents(new ChatListener(ipc), this);
		this.getServer().getPluginManager().registerEvents(new ConnectionListener(ipc), this);
	}

	@Override
	public void onDisable() {
	}
}
