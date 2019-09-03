using System;
using System.Collections.Generic;

[Serializable]
public class PairedDeviceModel{
	public string address;
	public string name;
}

public class PairedDevicesListModel {
	public List<PairedDeviceModel> devices;
}