﻿Shader "VacuumShaders/Terrain To Mesh/Standard/Diffuse/4 Textures" 
{
	Properties 
	{
		_Color("Tint Color", color) = (1, 1, 1, 1)
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_V_T2M_Control ("Control Map (RGBA)", 2D) = "black" {}

		//TTM	
		[V_T2M_SplatDiffuseMap] _V_T2M_Splat1 ("Layer 1 (R)", 2D) = "white" {}
		[HideInInspector] _V_T2M_Splat1_uvScale("", float) = 1	

		[V_T2M_SplatDiffuseMap] _V_T2M_Splat2 ("Layer 2 (G)", 2D) = "white" {}
		[HideInInspector] _V_T2M_Splat2_uvScale("", float) = 1	

		[V_T2M_SplatDiffuseMap] _V_T2M_Splat3 ("Layer 3 (B)", 2D) = "white" {}
		[HideInInspector] _V_T2M_Splat3_uvScale("", float) = 1	

		[V_T2M_SplatDiffuseMap] _V_T2M_Splat4 ("Layer 4 (A)", 2D) = "white" {}
		[HideInInspector] _V_T2M_Splat4_uvScale("", float) = 1	
	}

	SubShader 
	{
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows
		#pragma exclude_renderers flash

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0


		#define V_T2M_STANDARD
		#define V_T2M_3_TEX
		#define V_T2M_4_TEX

		#include "../cginc/T2M_Deferred.cginc"		

		ENDCG
	} 

	FallBack "VacuumShaders/Terrain To Mesh/Legacy Shaders/Diffuse/4 Textures" 
}