Shader "Custom/DisableZWriteTUT"
{
    Subshader{
        Tags{
            "RenderType" = "Opaque"
        }
        
        Pass{
            Zwrite Off
        }
    }
}