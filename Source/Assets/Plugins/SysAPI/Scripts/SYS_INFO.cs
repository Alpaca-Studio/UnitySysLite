using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;

namespace Sys {
	public class Info : MonoBehaviour {
		//System Information Methods//
        static string temporaryPath;
        static int itemCounter;
		
        public static float batteryLevel() { return SystemInfo.batteryLevel; }
        public static BatteryStatus batteryStatus() { return SystemInfo.batteryStatus; }
        public static UnityEngine.Rendering.CopyTextureSupport copyTextureSupport() { return SystemInfo.copyTextureSupport; }
        public static string deviceModel() { return SystemInfo.deviceModel; }
        public static string deviceName() { return SystemInfo.deviceName; }
        public static DeviceType deviceType() { return SystemInfo.deviceType; }
        public static string deviceUniqueIdentifier() { return SystemInfo.deviceUniqueIdentifier; }
        public static int graphicsDeviceID() { return SystemInfo.graphicsDeviceID; }
        public static string graphicsDeviceName() { return SystemInfo.graphicsDeviceName; }
        public static UnityEngine.Rendering.GraphicsDeviceType graphicsDeviceType() { return SystemInfo.graphicsDeviceType; }
        public static string graphicsDeviceVendor() { return SystemInfo.graphicsDeviceVendor; }
        public static int graphicsDeviceVendorID() { return SystemInfo.graphicsDeviceVendorID; }
        public static string graphicsDeviceVersion() { return SystemInfo.graphicsDeviceVersion; }
        public static int graphicsMemorySize() { return SystemInfo.graphicsMemorySize; }
        public static bool graphicsMultiThreaded() { return SystemInfo.graphicsMultiThreaded; }
        public static int graphicsShaderLevel() { return SystemInfo.graphicsShaderLevel; }
        public static bool graphicsUVStartsAtTop() { return SystemInfo.graphicsUVStartsAtTop; }
        public static int maxCubemapSize() { return SystemInfo.maxCubemapSize; }
        public static int maxTextureSize() { return SystemInfo.maxTextureSize; }
        public static NPOTSupport npotSupport() { return SystemInfo.npotSupport; }
        public static string operatingSystem() { return SystemInfo.operatingSystem; }
        public static OperatingSystemFamily operatingSystemFamily() { return SystemInfo.operatingSystemFamily; }
        public static int processorCount() { return SystemInfo.processorCount; }
        public static int processorFrequency() { return SystemInfo.processorFrequency; }
        public static string processorType() { return SystemInfo.processorType; }
        public static int supportedRenderTargetCount() { return SystemInfo.supportedRenderTargetCount; }
        public static bool supports2DArrayTextures() { return SystemInfo.supports2DArrayTextures; }
        public static bool supports3DRenderTextures() { return SystemInfo.supports3DRenderTextures; }
        public static bool supports3DTextures() { return SystemInfo.supports3DTextures; }
        public static bool supportsAccelerometer() { return SystemInfo.supportsAccelerometer; }
        public static bool supportsAudio() { return SystemInfo.supportsAudio; }
        public static bool supportsComputeShaders() { return SystemInfo.supportsComputeShaders; }
        public static bool supportsCubemapArrayTextures() { return SystemInfo.supportsCubemapArrayTextures; }
        public static bool supportsGyroscope() { return SystemInfo.supportsGyroscope; }
        public static bool supportsImageEffects() { return SystemInfo.supportsImageEffects; }
        public static bool supportsInstancing() { return SystemInfo.supportsInstancing; }
        public static bool supportsLocationService() { return SystemInfo.supportsLocationService; }
        public static bool supportsMotionVectors() { return SystemInfo.supportsMotionVectors; }
        public static bool supportsRawShadowDepthSampling() { return SystemInfo.supportsRawShadowDepthSampling; }
        public static bool supportsRenderToCubemap() { return SystemInfo.supportsRenderToCubemap; }
        public static bool supportsShadows() { return SystemInfo.supportsShadows; }
        public static bool supportsSparseTextures() { return SystemInfo.supportsSparseTextures; }
        public static bool supportsVibration() { return SystemInfo.supportsVibration; }
        public static int systemMemorySize() { return SystemInfo.systemMemorySize; }
        public static string unsupportedIdentifier() { return SystemInfo.unsupportedIdentifier; }
        public static bool usesReversedZBuffer() { return SystemInfo.usesReversedZBuffer; }

        public static void SaveSystemInfo(string path)
        {
           SaveSystemInfo(path, false);
        }

        public static void SaveSystemInfo(string path, bool openDir)
        {
			if(path != null){
				if (!File.Exists(path))
				{
					File.Create(path).Dispose();
					temporaryPath = path;
				}
				else
				{
					temporaryPath = path;
				}
				itemCounter = 0;
				WriteDataToFile(GetSystemInfo(), openDir);
			} else {
				Sys.Logging.LogError("[Sys API] ERROR004: Path is null or empty. (EC-SYS-"+Sys.Trace.GetLine()+")",true);
			}
        }

        static void WriteDataToFile(List<string> sysInfo, bool openDir)
        {
            foreach (string ab in sysInfo)
            {
                File.AppendAllText(temporaryPath, string.Format("{0} {1}", ab, System.Environment.NewLine));
                itemCounter++;
            }
            if (itemCounter >= (sysInfo.ToArray().Length)) { itemCounter = 0; sysInfo.Clear(); }
            if (openDir) { System.Diagnostics.Process.Start(temporaryPath); } //Debug.Log("[Sys]: Opening File Location...");}	
        }
		

        public static List<string> GetSystemInfo()
        {
            List<string> sysInfo = new List<string>();
            sysInfo.Add("Battery Level: " + (SystemInfo.batteryLevel * 100) + "%");
            sysInfo.Add("Battery Status: " + SystemInfo.batteryStatus.ToString());
            sysInfo.Add("Copy Texture Support: " + SystemInfo.copyTextureSupport.ToString());
            sysInfo.Add("Device Model: " + SystemInfo.deviceModel);
            sysInfo.Add("Device Name: " + SystemInfo.deviceName);
            sysInfo.Add("Device Type: " + SystemInfo.deviceType.ToString());
            sysInfo.Add("Unique Device ID: " + SystemInfo.deviceUniqueIdentifier);
            sysInfo.Add("Graphics Device ID: " + SystemInfo.graphicsDeviceID);
            sysInfo.Add("Graphics Device Name: " + SystemInfo.graphicsDeviceName);
            sysInfo.Add("Graphics Device Type: " + SystemInfo.graphicsDeviceType.ToString());
            sysInfo.Add("Graphics Device Vendor: " + SystemInfo.graphicsDeviceVendor);
            sysInfo.Add("Graphics Device VendorID: " + SystemInfo.graphicsDeviceVendorID);
            sysInfo.Add("Graphics Device Version: " + SystemInfo.graphicsDeviceVersion);
            sysInfo.Add("Graphics Memory Size: " + SystemInfo.graphicsMemorySize);//
            sysInfo.Add("Graphics Multithreaded: " + SystemInfo.graphicsMultiThreaded);
            sysInfo.Add("Graphics Shader Level: " + SystemInfo.graphicsShaderLevel);
            sysInfo.Add("Graphics UV Starts at Top?: " + SystemInfo.graphicsUVStartsAtTop);
            sysInfo.Add("Max Cubemap Size: " + SystemInfo.maxCubemapSize);
            sysInfo.Add("Max Texture Size: " + SystemInfo.maxTextureSize);
            sysInfo.Add("NPOT Support: " + SystemInfo.npotSupport.ToString());
            sysInfo.Add("OS: " + SystemInfo.operatingSystem);
            sysInfo.Add("OS Family: " + SystemInfo.operatingSystemFamily.ToString());
            sysInfo.Add("Processor Count: " + SystemInfo.processorCount);
            sysInfo.Add("Processor Frequency: " + (SystemInfo.processorFrequency * 0.001) + "MHz");
            sysInfo.Add("Processor Type: " + SystemInfo.processorType);
            sysInfo.Add("Supported Render Targer Count: " + SystemInfo.supportedRenderTargetCount);
            sysInfo.Add("Supports 2D Array Textures : " + (SystemInfo.supports2DArrayTextures ? "Yes" : "No"));
            sysInfo.Add("Supports 3D Render Textures : " + (SystemInfo.supports3DRenderTextures ? "Yes" : "No"));
            sysInfo.Add("Supports 3D Textures : " + (SystemInfo.supports3DTextures ? "Yes" : "No"));
            sysInfo.Add("Supports Accelerometer : " + (SystemInfo.supportsAccelerometer ? "Yes" : "No"));
            sysInfo.Add("Supports Audio : " + (SystemInfo.supportsAudio ? "Yes" : "No"));
            sysInfo.Add("Supports Compute Shaders : " + (SystemInfo.supportsComputeShaders ? "Yes" : "No"));
            sysInfo.Add("Supports Cubemap Array Textures : " + (SystemInfo.supportsCubemapArrayTextures ? "Yes" : "No"));
            sysInfo.Add("Supports Gyroscope : " + (SystemInfo.supportsGyroscope ? "Yes" : "No"));
            sysInfo.Add("Supports Image Effects : " + (SystemInfo.supportsImageEffects ? "Yes" : "No"));
            sysInfo.Add("Supports Instancing : " + (SystemInfo.supportsInstancing ? "Yes" : "No"));
            sysInfo.Add("Supports Location Services : " + (SystemInfo.supportsLocationService ? "Yes" : "No"));
            sysInfo.Add("Supports Motion Vectors : " + (SystemInfo.supportsMotionVectors ? "Yes" : "No"));
            sysInfo.Add("Supports Raw Shadow Depth Sampling : " + (SystemInfo.supportsRawShadowDepthSampling ? "Yes" : "No"));
            sysInfo.Add("Supports Render To Cubemap : " + (SystemInfo.supportsRenderToCubemap ? "Yes" : "No"));
            sysInfo.Add("Supports Shadows : " + (SystemInfo.supportsShadows ? "Yes" : "No"));
            sysInfo.Add("Supports Sparse Textures : " + (SystemInfo.supportsSparseTextures ? "Yes" : "No"));
            sysInfo.Add("Supports Vibration : " + (SystemInfo.supportsVibration ? "Yes" : "No"));
            sysInfo.Add("System Memory Size: " + SystemInfo.systemMemorySize);
            sysInfo.Add("Unsupported Identifier: " + SystemInfo.unsupportedIdentifier);
            sysInfo.Add("Supports Reversed Z Buffer : " + (SystemInfo.usesReversedZBuffer ? "Yes" : "No"));

            return sysInfo;
        }
		
	}
	
}