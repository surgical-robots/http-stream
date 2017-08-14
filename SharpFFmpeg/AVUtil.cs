using System;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Security;

namespace SharpFFmpeg
{
    public partial class FFmpeg
    {
        [DllImport("avutil.dll"), SuppressUnmanagedCodeSecurity]
        public static extern AVFrame av_frame_alloc();

        [DllImport("avutil.dll"), SuppressUnmanagedCodeSecurity]
        public static extern int av_frame_get_buffer(AVFrame pAVFrame, int align);

        [DllImport("avutil.dll"), SuppressUnmanagedCodeSecurity]
        public static extern int av_frame_make_writable(AVFrame pAVFrame);

        [DllImport("avutil.dll"), SuppressUnmanagedCodeSecurity]
        public static extern void av_frame_free(AVFrame pAVFrame);

        public delegate int Read_PacketCallback(IntPtr opaque, IntPtr buf, int buf_size);

        public delegate int WritePacketCallback(IntPtr opaque, IntPtr buf, int buf_size);

        public delegate Int64 SeekCallback(IntPtr opaque, Int64 offset, int whence);

        public delegate UInt32 UpdateChecksumCallback(UInt32 checksum, IntPtr buf, UInt32 size);

        // * Pixel format.
        // *
        // * @note
        // * AV_PIX_FMT_RGB32 is handled in an endian-specific manner. An RGBA
        // * color is put together as:
        // *  (A << 24) | (R << 16) | (G << 8) | B
        // * This is stored as BGRA on little-endian CPU architectures and ARGB on
        // * big-endian CPUs.
        // *
        // * @par
        // * When the pixel format is palettized RGB32 (AV_PIX_FMT_PAL8), the palettized
        // * image data is stored in AVFrame.data[0]. The palette is transported in
        // * AVFrame.data[1], is 1024 bytes long (256 4-byte entries) and is
        // * formatted the same as in AV_PIX_FMT_RGB32 described above(i.e., it is
        // * also endian-specific). Note also that the individual RGB32 palette
        // * components stored in AVFrame.data[1] should be in the range 0..255.
        // * This is important as many custom PAL8 video codecs that were designed
        // * to run on the IBM VGA graphics adapter use 6-bit palette components.
        // *
        // * @par
        // * For all the 8 bits per pixel formats, an RGB32 palette is in data[1] like
        // * for pal8.This palette is filled in automatically by the function
        // * allocating the picture.
        public enum AVPixelFormat
        {
            AV_PIX_FMT_NONE = -1,
            AV_PIX_FMT_YUV420P,     //< planar YUV 4:2:0, 12bpp, (1 Cr & Cb sample per 2x2 Y samples)
            AV_PIX_FMT_YUYV422,     //< packed YUV 4:2:2, 16bpp, Y0 Cb Y1 Cr
            AV_PIX_FMT_RGB24,       //< packed RGB 8:8:8, 24bpp, RGBRGB...
            AV_PIX_FMT_BGR24,       //< packed RGB 8:8:8, 24bpp, BGRBGR...
            AV_PIX_FMT_YUV422P,     //< planar YUV 4:2:2, 16bpp, (1 Cr & Cb sample per 2x1 Y samples)
            AV_PIX_FMT_YUV444P,     //< planar YUV 4:4:4, 24bpp, (1 Cr & Cb sample per 1x1 Y samples)
            AV_PIX_FMT_YUV410P,     //< planar YUV 4:1:0,  9bpp, (1 Cr & Cb sample per 4x4 Y samples)
            AV_PIX_FMT_YUV411P,     //< planar YUV 4:1:1, 12bpp, (1 Cr & Cb sample per 4x1 Y samples)
            AV_PIX_FMT_GRAY8,       //<        Y        ,  8bpp
            AV_PIX_FMT_MONOWHITE,   //<        Y        ,  1bpp, 0 is white, 1 is black, in each byte pixels are ordered from the msb to the lsb
            AV_PIX_FMT_MONOBLACK,   //<        Y        ,  1bpp, 0 is black, 1 is white, in each byte pixels are ordered from the msb to the lsb
            AV_PIX_FMT_PAL8,        //< 8 bits with AV_PIX_FMT_RGB32 palette
            AV_PIX_FMT_YUVJ420P,    //< planar YUV 4:2:0, 12bpp, full scale (JPEG), deprecated in favor of AV_PIX_FMT_YUV420P and setting color_range
            AV_PIX_FMT_YUVJ422P,    //< planar YUV 4:2:2, 16bpp, full scale (JPEG), deprecated in favor of AV_PIX_FMT_YUV422P and setting color_range
            AV_PIX_FMT_YUVJ444P,    //< planar YUV 4:4:4, 24bpp, full scale (JPEG), deprecated in favor of AV_PIX_FMT_YUV444P and setting color_range
//#if FF_API_XVMC
            AV_PIX_FMT_XVMC_MPEG2_MC,   //< XVideo Motion Acceleration via common packet passing
            AV_PIX_FMT_XVMC_MPEG2_IDCT,
            AV_PIX_FMT_XVMC = AV_PIX_FMT_XVMC_MPEG2_IDCT,
//#endif /* FF_API_XVMC */
            AV_PIX_FMT_UYVY422,     //< packed YUV 4:2:2, 16bpp, Cb Y0 Cr Y1
            AV_PIX_FMT_UYYVYY411,   //< packed YUV 4:1:1, 12bpp, Cb Y0 Y1 Cr Y2 Y3
            AV_PIX_FMT_BGR8,        //< packed RGB 3:3:2,  8bpp, (msb)2B 3G 3R(lsb)
            AV_PIX_FMT_BGR4,        //< packed RGB 1:2:1 bitstream,  4bpp, (msb)1B 2G 1R(lsb), a byte contains two pixels, the first pixel in the byte is the one composed by the 4 msb bits
            AV_PIX_FMT_BGR4_BYTE,   //< packed RGB 1:2:1,  8bpp, (msb)1B 2G 1R(lsb)
            AV_PIX_FMT_RGB8,        //< packed RGB 3:3:2,  8bpp, (msb)2R 3G 3B(lsb)
            AV_PIX_FMT_RGB4,        //< packed RGB 1:2:1 bitstream,  4bpp, (msb)1R 2G 1B(lsb), a byte contains two pixels, the first pixel in the byte is the one composed by the 4 msb bits
            AV_PIX_FMT_RGB4_BYTE,   //< packed RGB 1:2:1,  8bpp, (msb)1R 2G 1B(lsb)
            AV_PIX_FMT_NV12,        //< planar YUV 4:2:0, 12bpp, 1 plane for Y and 1 plane for the UV components, which are interleaved (first byte U and the following byte V)
            AV_PIX_FMT_NV21,        //< as above, but U and V bytes are swapped

            AV_PIX_FMT_ARGB,        //< packed ARGB 8:8:8:8, 32bpp, ARGBARGB...
            AV_PIX_FMT_RGBA,        //< packed RGBA 8:8:8:8, 32bpp, RGBARGBA...
            AV_PIX_FMT_ABGR,        //< packed ABGR 8:8:8:8, 32bpp, ABGRABGR...
            AV_PIX_FMT_BGRA,        //< packed BGRA 8:8:8:8, 32bpp, BGRABGRA...

            AV_PIX_FMT_GRAY16BE,    //<        Y        , 16bpp, big-endian
            AV_PIX_FMT_GRAY16LE,    //<        Y        , 16bpp, little-endian
            AV_PIX_FMT_YUV440P,     //< planar YUV 4:4:0 (1 Cr & Cb sample per 1x2 Y samples)
            AV_PIX_FMT_YUVJ440P,    //< planar YUV 4:4:0 full scale (JPEG), deprecated in favor of AV_PIX_FMT_YUV440P and setting color_range
            AV_PIX_FMT_YUVA420P,    //< planar YUV 4:2:0, 20bpp, (1 Cr & Cb sample per 2x2 Y & A samples)
//#if FF_API_VDPAU
            AV_PIX_FMT_VDPAU_H264,  //< H.264 HW decoding with VDPAU, data[0] contains a vdpau_render_state struct which contains the bitstream of the slices as well as various fields extracted from headers
            AV_PIX_FMT_VDPAU_MPEG1, //< MPEG-1 HW decoding with VDPAU, data[0] contains a vdpau_render_state struct which contains the bitstream of the slices as well as various fields extracted from headers
            AV_PIX_FMT_VDPAU_MPEG2, //< MPEG-2 HW decoding with VDPAU, data[0] contains a vdpau_render_state struct which contains the bitstream of the slices as well as various fields extracted from headers
            AV_PIX_FMT_VDPAU_WMV3,  //< WMV3 HW decoding with VDPAU, data[0] contains a vdpau_render_state struct which contains the bitstream of the slices as well as various fields extracted from headers
            AV_PIX_FMT_VDPAU_VC1,   //< VC-1 HW decoding with VDPAU, data[0] contains a vdpau_render_state struct which contains the bitstream of the slices as well as various fields extracted from headers
//#endif
            AV_PIX_FMT_RGB48BE,     //< packed RGB 16:16:16, 48bpp, 16R, 16G, 16B, the 2-byte value for each R/G/B component is stored as big-endian
            AV_PIX_FMT_RGB48LE,     //< packed RGB 16:16:16, 48bpp, 16R, 16G, 16B, the 2-byte value for each R/G/B component is stored as little-endian

            AV_PIX_FMT_RGB565BE,    //< packed RGB 5:6:5, 16bpp, (msb)   5R 6G 5B(lsb), big-endian
            AV_PIX_FMT_RGB565LE,    //< packed RGB 5:6:5, 16bpp, (msb)   5R 6G 5B(lsb), little-endian
            AV_PIX_FMT_RGB555BE,    //< packed RGB 5:5:5, 16bpp, (msb)1X 5R 5G 5B(lsb), big-endian   , X=unused/undefined
            AV_PIX_FMT_RGB555LE,    //< packed RGB 5:5:5, 16bpp, (msb)1X 5R 5G 5B(lsb), little-endian, X=unused/undefined

            AV_PIX_FMT_BGR565BE,    //< packed BGR 5:6:5, 16bpp, (msb)   5B 6G 5R(lsb), big-endian
            AV_PIX_FMT_BGR565LE,    //< packed BGR 5:6:5, 16bpp, (msb)   5B 6G 5R(lsb), little-endian
            AV_PIX_FMT_BGR555BE,    //< packed BGR 5:5:5, 16bpp, (msb)1X 5B 5G 5R(lsb), big-endian   , X=unused/undefined
            AV_PIX_FMT_BGR555LE,    //< packed BGR 5:5:5, 16bpp, (msb)1X 5B 5G 5R(lsb), little-endian, X=unused/undefined

//#if FF_API_VAAPI
//          /** @name Deprecated pixel formats */
//          /**@{*/
//          AV_PIX_FMT_VAAPI_MOCO, ///< HW acceleration through VA API at motion compensation entry-point, Picture.data[3] contains a vaapi_render_state struct which contains macroblocks as well as various fields extracted from headers
//          AV_PIX_FMT_VAAPI_IDCT, ///< HW acceleration through VA API at IDCT entry-point, Picture.data[3] contains a vaapi_render_state struct which contains fields extracted from headers
//          AV_PIX_FMT_VAAPI_VLD,  ///< HW decoding through VA API, Picture.data[3] contains a VASurfaceID
//          /**@}*/
//          AV_PIX_FMT_VAAPI = AV_PIX_FMT_VAAPI_VLD,
//#else
            /**
             *  Hardware acceleration through VA-API, data[3] contains a
             *  VASurfaceID.
             */
            AV_PIX_FMT_VAAPI,
//#endif

            AV_PIX_FMT_YUV420P16LE,     //< planar YUV 4:2:0, 24bpp, (1 Cr & Cb sample per 2x2 Y samples), little-endian
            AV_PIX_FMT_YUV420P16BE,     //< planar YUV 4:2:0, 24bpp, (1 Cr & Cb sample per 2x2 Y samples), big-endian
            AV_PIX_FMT_YUV422P16LE,     //< planar YUV 4:2:2, 32bpp, (1 Cr & Cb sample per 2x1 Y samples), little-endian
            AV_PIX_FMT_YUV422P16BE,     //< planar YUV 4:2:2, 32bpp, (1 Cr & Cb sample per 2x1 Y samples), big-endian
            AV_PIX_FMT_YUV444P16LE,     //< planar YUV 4:4:4, 48bpp, (1 Cr & Cb sample per 1x1 Y samples), little-endian
            AV_PIX_FMT_YUV444P16BE,     //< planar YUV 4:4:4, 48bpp, (1 Cr & Cb sample per 1x1 Y samples), big-endian
//#if FF_API_VDPAU
            AV_PIX_FMT_VDPAU_MPEG4,     //< MPEG-4 HW decoding with VDPAU, data[0] contains a vdpau_render_state struct which contains the bitstream of the slices as well as various fields extracted from headers
//#endif
            AV_PIX_FMT_DXVA2_VLD,       //< HW decoding through DXVA2, Picture.data[3] contains a LPDIRECT3DSURFACE9 pointer

            AV_PIX_FMT_RGB444LE,        //< packed RGB 4:4:4, 16bpp, (msb)4X 4R 4G 4B(lsb), little-endian, X=unused/undefined
            AV_PIX_FMT_RGB444BE,        //< packed RGB 4:4:4, 16bpp, (msb)4X 4R 4G 4B(lsb), big-endian,    X=unused/undefined
            AV_PIX_FMT_BGR444LE,        //< packed BGR 4:4:4, 16bpp, (msb)4X 4B 4G 4R(lsb), little-endian, X=unused/undefined
            AV_PIX_FMT_BGR444BE,        //< packed BGR 4:4:4, 16bpp, (msb)4X 4B 4G 4R(lsb), big-endian,    X=unused/undefined
            AV_PIX_FMT_YA8,             //< 8 bits gray, 8 bits alpha

            AV_PIX_FMT_Y400A = AV_PIX_FMT_YA8,  //< alias for AV_PIX_FMT_YA8
            AV_PIX_FMT_GRAY8A = AV_PIX_FMT_YA8, //< alias for AV_PIX_FMT_YA8

            AV_PIX_FMT_BGR48BE,         //< packed RGB 16:16:16, 48bpp, 16B, 16G, 16R, the 2-byte value for each R/G/B component is stored as big-endian
            AV_PIX_FMT_BGR48LE,         //< packed RGB 16:16:16, 48bpp, 16B, 16G, 16R, the 2-byte value for each R/G/B component is stored as little-endian

            /**
             * The following 12 formats have the disadvantage of needing 1 format for each bit depth.
             * Notice that each 9/10 bits sample is stored in 16 bits with extra padding.
             * If you want to support multiple bit depths, then using AV_PIX_FMT_YUV420P16* with the bpp stored separately is better.
             */
            AV_PIX_FMT_YUV420P9BE,      //< planar YUV 4:2:0, 13.5bpp, (1 Cr & Cb sample per 2x2 Y samples), big-endian
            AV_PIX_FMT_YUV420P9LE,      //< planar YUV 4:2:0, 13.5bpp, (1 Cr & Cb sample per 2x2 Y samples), little-endian
            AV_PIX_FMT_YUV420P10BE,     //< planar YUV 4:2:0, 15bpp, (1 Cr & Cb sample per 2x2 Y samples), big-endian
            AV_PIX_FMT_YUV420P10LE,     //< planar YUV 4:2:0, 15bpp, (1 Cr & Cb sample per 2x2 Y samples), little-endian
            AV_PIX_FMT_YUV422P10BE,     //< planar YUV 4:2:2, 20bpp, (1 Cr & Cb sample per 2x1 Y samples), big-endian
            AV_PIX_FMT_YUV422P10LE,     //< planar YUV 4:2:2, 20bpp, (1 Cr & Cb sample per 2x1 Y samples), little-endian
            AV_PIX_FMT_YUV444P9BE,      //< planar YUV 4:4:4, 27bpp, (1 Cr & Cb sample per 1x1 Y samples), big-endian
            AV_PIX_FMT_YUV444P9LE,      //< planar YUV 4:4:4, 27bpp, (1 Cr & Cb sample per 1x1 Y samples), little-endian
            AV_PIX_FMT_YUV444P10BE,     //< planar YUV 4:4:4, 30bpp, (1 Cr & Cb sample per 1x1 Y samples), big-endian
            AV_PIX_FMT_YUV444P10LE,     //< planar YUV 4:4:4, 30bpp, (1 Cr & Cb sample per 1x1 Y samples), little-endian
            AV_PIX_FMT_YUV422P9BE,      //< planar YUV 4:2:2, 18bpp, (1 Cr & Cb sample per 2x1 Y samples), big-endian
            AV_PIX_FMT_YUV422P9LE,      //< planar YUV 4:2:2, 18bpp, (1 Cr & Cb sample per 2x1 Y samples), little-endian
            AV_PIX_FMT_VDA_VLD,         //< hardware decoding through VDA
            AV_PIX_FMT_GBRP,            //< planar GBR 4:4:4 24bpp
            AV_PIX_FMT_GBR24P = AV_PIX_FMT_GBRP, // alias for #AV_PIX_FMT_GBRP
            AV_PIX_FMT_GBRP9BE,         //< planar GBR 4:4:4 27bpp, big-endian
            AV_PIX_FMT_GBRP9LE,         //< planar GBR 4:4:4 27bpp, little-endian
            AV_PIX_FMT_GBRP10BE,        //< planar GBR 4:4:4 30bpp, big-endian
            AV_PIX_FMT_GBRP10LE,        //< planar GBR 4:4:4 30bpp, little-endian
            AV_PIX_FMT_GBRP16BE,        //< planar GBR 4:4:4 48bpp, big-endian
            AV_PIX_FMT_GBRP16LE,        //< planar GBR 4:4:4 48bpp, little-endian
            AV_PIX_FMT_YUVA422P,        //< planar YUV 4:2:2 24bpp, (1 Cr & Cb sample per 2x1 Y & A samples)
            AV_PIX_FMT_YUVA444P,        //< planar YUV 4:4:4 32bpp, (1 Cr & Cb sample per 1x1 Y & A samples)
            AV_PIX_FMT_YUVA420P9BE,     //< planar YUV 4:2:0 22.5bpp, (1 Cr & Cb sample per 2x2 Y & A samples), big-endian
            AV_PIX_FMT_YUVA420P9LE,     //< planar YUV 4:2:0 22.5bpp, (1 Cr & Cb sample per 2x2 Y & A samples), little-endian
            AV_PIX_FMT_YUVA422P9BE,     //< planar YUV 4:2:2 27bpp, (1 Cr & Cb sample per 2x1 Y & A samples), big-endian
            AV_PIX_FMT_YUVA422P9LE,     //< planar YUV 4:2:2 27bpp, (1 Cr & Cb sample per 2x1 Y & A samples), little-endian
            AV_PIX_FMT_YUVA444P9BE,     //< planar YUV 4:4:4 36bpp, (1 Cr & Cb sample per 1x1 Y & A samples), big-endian
            AV_PIX_FMT_YUVA444P9LE,     //< planar YUV 4:4:4 36bpp, (1 Cr & Cb sample per 1x1 Y & A samples), little-endian
            AV_PIX_FMT_YUVA420P10BE,    //< planar YUV 4:2:0 25bpp, (1 Cr & Cb sample per 2x2 Y & A samples, big-endian)
            AV_PIX_FMT_YUVA420P10LE,    //< planar YUV 4:2:0 25bpp, (1 Cr & Cb sample per 2x2 Y & A samples, little-endian)
            AV_PIX_FMT_YUVA422P10BE,    //< planar YUV 4:2:2 30bpp, (1 Cr & Cb sample per 2x1 Y & A samples, big-endian)
            AV_PIX_FMT_YUVA422P10LE,    //< planar YUV 4:2:2 30bpp, (1 Cr & Cb sample per 2x1 Y & A samples, little-endian)
            AV_PIX_FMT_YUVA444P10BE,    //< planar YUV 4:4:4 40bpp, (1 Cr & Cb sample per 1x1 Y & A samples, big-endian)
            AV_PIX_FMT_YUVA444P10LE,    //< planar YUV 4:4:4 40bpp, (1 Cr & Cb sample per 1x1 Y & A samples, little-endian)
            AV_PIX_FMT_YUVA420P16BE,    //< planar YUV 4:2:0 40bpp, (1 Cr & Cb sample per 2x2 Y & A samples, big-endian)
            AV_PIX_FMT_YUVA420P16LE,    //< planar YUV 4:2:0 40bpp, (1 Cr & Cb sample per 2x2 Y & A samples, little-endian)
            AV_PIX_FMT_YUVA422P16BE,    //< planar YUV 4:2:2 48bpp, (1 Cr & Cb sample per 2x1 Y & A samples, big-endian)
            AV_PIX_FMT_YUVA422P16LE,    //< planar YUV 4:2:2 48bpp, (1 Cr & Cb sample per 2x1 Y & A samples, little-endian)
            AV_PIX_FMT_YUVA444P16BE,    //< planar YUV 4:4:4 64bpp, (1 Cr & Cb sample per 1x1 Y & A samples, big-endian)
            AV_PIX_FMT_YUVA444P16LE,    //< planar YUV 4:4:4 64bpp, (1 Cr & Cb sample per 1x1 Y & A samples, little-endian)

            AV_PIX_FMT_VDPAU,           //< HW acceleration through VDPAU, Picture.data[3] contains a VdpVideoSurface

            AV_PIX_FMT_XYZ12LE,         //< packed XYZ 4:4:4, 36 bpp, (msb) 12X, 12Y, 12Z (lsb), the 2-byte value for each X/Y/Z is stored as little-endian, the 4 lower bits are set to 0
            AV_PIX_FMT_XYZ12BE,         //< packed XYZ 4:4:4, 36 bpp, (msb) 12X, 12Y, 12Z (lsb), the 2-byte value for each X/Y/Z is stored as big-endian, the 4 lower bits are set to 0
            AV_PIX_FMT_NV16,            //< interleaved chroma YUV 4:2:2, 16bpp, (1 Cr & Cb sample per 2x1 Y samples)
            AV_PIX_FMT_NV20LE,          //< interleaved chroma YUV 4:2:2, 20bpp, (1 Cr & Cb sample per 2x1 Y samples), little-endian
            AV_PIX_FMT_NV20BE,          //< interleaved chroma YUV 4:2:2, 20bpp, (1 Cr & Cb sample per 2x1 Y samples), big-endian

            AV_PIX_FMT_RGBA64BE,        //< packed RGBA 16:16:16:16, 64bpp, 16R, 16G, 16B, 16A, the 2-byte value for each R/G/B/A component is stored as big-endian
            AV_PIX_FMT_RGBA64LE,        //< packed RGBA 16:16:16:16, 64bpp, 16R, 16G, 16B, 16A, the 2-byte value for each R/G/B/A component is stored as little-endian
            AV_PIX_FMT_BGRA64BE,        //< packed RGBA 16:16:16:16, 64bpp, 16B, 16G, 16R, 16A, the 2-byte value for each R/G/B/A component is stored as big-endian
            AV_PIX_FMT_BGRA64LE,        //< packed RGBA 16:16:16:16, 64bpp, 16B, 16G, 16R, 16A, the 2-byte value for each R/G/B/A component is stored as little-endian

            AV_PIX_FMT_YVYU422,         //< packed YUV 4:2:2, 16bpp, Y0 Cr Y1 Cb

            AV_PIX_FMT_VDA,             //< HW acceleration through VDA, data[3] contains a CVPixelBufferRef

            AV_PIX_FMT_YA16BE,          //< 16 bits gray, 16 bits alpha (big-endian)
            AV_PIX_FMT_YA16LE,          //< 16 bits gray, 16 bits alpha (little-endian)

            AV_PIX_FMT_GBRAP,           //< planar GBRA 4:4:4:4 32bpp
            AV_PIX_FMT_GBRAP16BE,       //< planar GBRA 4:4:4:4 64bpp, big-endian
            AV_PIX_FMT_GBRAP16LE,       //< planar GBRA 4:4:4:4 64bpp, little-endian
            /**
             *  HW acceleration through QSV, data[3] contains a pointer to the
             *  mfxFrameSurface1 structure.
             */
            AV_PIX_FMT_QSV,
            /**
             * HW acceleration though MMAL, data[3] contains a pointer to the
             * MMAL_BUFFER_HEADER_T structure.
             */
            AV_PIX_FMT_MMAL,

            AV_PIX_FMT_D3D11VA_VLD,  //< HW decoding through Direct3D11 via old API, Picture.data[3] contains a ID3D11VideoDecoderOutputView pointer

            /**
             * HW acceleration through CUDA. data[i] contain CUdeviceptr pointers
             * exactly as for system memory frames.
             */
            AV_PIX_FMT_CUDA,

            AV_PIX_FMT_0RGB = 0x123 + 4,//< packed RGB 8:8:8, 32bpp, XRGBXRGB...   X=unused/undefined
            AV_PIX_FMT_RGB0,            //< packed RGB 8:8:8, 32bpp, RGBXRGBX...   X=unused/undefined
            AV_PIX_FMT_0BGR,            //< packed BGR 8:8:8, 32bpp, XBGRXBGR...   X=unused/undefined
            AV_PIX_FMT_BGR0,            //< packed BGR 8:8:8, 32bpp, BGRXBGRX...   X=unused/undefined

            AV_PIX_FMT_YUV420P12BE,     //< planar YUV 4:2:0,18bpp, (1 Cr & Cb sample per 2x2 Y samples), big-endian
            AV_PIX_FMT_YUV420P12LE,     //< planar YUV 4:2:0,18bpp, (1 Cr & Cb sample per 2x2 Y samples), little-endian
            AV_PIX_FMT_YUV420P14BE,     //< planar YUV 4:2:0,21bpp, (1 Cr & Cb sample per 2x2 Y samples), big-endian
            AV_PIX_FMT_YUV420P14LE,     //< planar YUV 4:2:0,21bpp, (1 Cr & Cb sample per 2x2 Y samples), little-endian
            AV_PIX_FMT_YUV422P12BE,     //< planar YUV 4:2:2,24bpp, (1 Cr & Cb sample per 2x1 Y samples), big-endian
            AV_PIX_FMT_YUV422P12LE,     //< planar YUV 4:2:2,24bpp, (1 Cr & Cb sample per 2x1 Y samples), little-endian
            AV_PIX_FMT_YUV422P14BE,     //< planar YUV 4:2:2,28bpp, (1 Cr & Cb sample per 2x1 Y samples), big-endian
            AV_PIX_FMT_YUV422P14LE,     //< planar YUV 4:2:2,28bpp, (1 Cr & Cb sample per 2x1 Y samples), little-endian
            AV_PIX_FMT_YUV444P12BE,     //< planar YUV 4:4:4,36bpp, (1 Cr & Cb sample per 1x1 Y samples), big-endian
            AV_PIX_FMT_YUV444P12LE,     //< planar YUV 4:4:4,36bpp, (1 Cr & Cb sample per 1x1 Y samples), little-endian
            AV_PIX_FMT_YUV444P14BE,     //< planar YUV 4:4:4,42bpp, (1 Cr & Cb sample per 1x1 Y samples), big-endian
            AV_PIX_FMT_YUV444P14LE,     //< planar YUV 4:4:4,42bpp, (1 Cr & Cb sample per 1x1 Y samples), little-endian
            AV_PIX_FMT_GBRP12BE,        //< planar GBR 4:4:4 36bpp, big-endian
            AV_PIX_FMT_GBRP12LE,        //< planar GBR 4:4:4 36bpp, little-endian
            AV_PIX_FMT_GBRP14BE,        //< planar GBR 4:4:4 42bpp, big-endian
            AV_PIX_FMT_GBRP14LE,        //< planar GBR 4:4:4 42bpp, little-endian
            AV_PIX_FMT_YUVJ411P,        //< planar YUV 4:1:1, 12bpp, (1 Cr & Cb sample per 4x1 Y samples) full scale (JPEG), deprecated in favor of AV_PIX_FMT_YUV411P and setting color_range

            AV_PIX_FMT_BAYER_BGGR8,     //< bayer, BGBG..(odd line), GRGR..(even line), 8-bit samples */
            AV_PIX_FMT_BAYER_RGGB8,     //< bayer, RGRG..(odd line), GBGB..(even line), 8-bit samples */
            AV_PIX_FMT_BAYER_GBRG8,     //< bayer, GBGB..(odd line), RGRG..(even line), 8-bit samples */
            AV_PIX_FMT_BAYER_GRBG8,     //< bayer, GRGR..(odd line), BGBG..(even line), 8-bit samples */
            AV_PIX_FMT_BAYER_BGGR16LE,  //< bayer, BGBG..(odd line), GRGR..(even line), 16-bit samples, little-endian */
            AV_PIX_FMT_BAYER_BGGR16BE,  //< bayer, BGBG..(odd line), GRGR..(even line), 16-bit samples, big-endian */
            AV_PIX_FMT_BAYER_RGGB16LE,  //< bayer, RGRG..(odd line), GBGB..(even line), 16-bit samples, little-endian */
            AV_PIX_FMT_BAYER_RGGB16BE,  //< bayer, RGRG..(odd line), GBGB..(even line), 16-bit samples, big-endian */
            AV_PIX_FMT_BAYER_GBRG16LE,  //< bayer, GBGB..(odd line), RGRG..(even line), 16-bit samples, little-endian */
            AV_PIX_FMT_BAYER_GBRG16BE,  //< bayer, GBGB..(odd line), RGRG..(even line), 16-bit samples, big-endian */
            AV_PIX_FMT_BAYER_GRBG16LE,  //< bayer, GRGR..(odd line), BGBG..(even line), 16-bit samples, little-endian */
            AV_PIX_FMT_BAYER_GRBG16BE,  //< bayer, GRGR..(odd line), BGBG..(even line), 16-bit samples, big-endian */
//#if !FF_API_XVMC
            //AV_PIX_FMT_XVMC,            //< XVideo Motion Acceleration via common packet passing
//#endif /* !FF_API_XVMC */
            AV_PIX_FMT_YUV440P10LE,     //< planar YUV 4:4:0,20bpp, (1 Cr & Cb sample per 1x2 Y samples), little-endian
            AV_PIX_FMT_YUV440P10BE,     //< planar YUV 4:4:0,20bpp, (1 Cr & Cb sample per 1x2 Y samples), big-endian
            AV_PIX_FMT_YUV440P12LE,     //< planar YUV 4:4:0,24bpp, (1 Cr & Cb sample per 1x2 Y samples), little-endian
            AV_PIX_FMT_YUV440P12BE,     //< planar YUV 4:4:0,24bpp, (1 Cr & Cb sample per 1x2 Y samples), big-endian
            AV_PIX_FMT_AYUV64LE,        //< packed AYUV 4:4:4,64bpp (1 Cr & Cb sample per 1x1 Y & A samples), little-endian
            AV_PIX_FMT_AYUV64BE,        //< packed AYUV 4:4:4,64bpp (1 Cr & Cb sample per 1x1 Y & A samples), big-endian

            AV_PIX_FMT_VIDEOTOOLBOX,    //< hardware decoding through Videotoolbox

            AV_PIX_FMT_P010LE,          //< like NV12, with 10bpp per component, data in the high bits, zeros in the low bits, little-endian
            AV_PIX_FMT_P010BE,          //< like NV12, with 10bpp per component, data in the high bits, zeros in the low bits, big-endian

            AV_PIX_FMT_GBRAP12BE,       //< planar GBR 4:4:4:4 48bpp, big-endian
            AV_PIX_FMT_GBRAP12LE,       //< planar GBR 4:4:4:4 48bpp, little-endian

            AV_PIX_FMT_GBRAP10BE,       //< planar GBR 4:4:4:4 40bpp, big-endian
            AV_PIX_FMT_GBRAP10LE,       //< planar GBR 4:4:4:4 40bpp, little-endian

            AV_PIX_FMT_MEDIACODEC,      //< hardware decoding through MediaCodec

            AV_PIX_FMT_GRAY12BE,        //<        Y        , 12bpp, big-endian
            AV_PIX_FMT_GRAY12LE,        //<        Y        , 12bpp, little-endian
            AV_PIX_FMT_GRAY10BE,        //<        Y        , 10bpp, big-endian
            AV_PIX_FMT_GRAY10LE,        //<        Y        , 10bpp, little-endian

            AV_PIX_FMT_P016LE,          //< like NV12, with 16bpp per component, little-endian
            AV_PIX_FMT_P016BE,          //< like NV12, with 16bpp per component, big-endian

            /**
             * Hardware surfaces for Direct3D11.
             *
             * This is preferred over the legacy AV_PIX_FMT_D3D11VA_VLD. The new D3D11
             * hwaccel API and filtering support AV_PIX_FMT_D3D11 only.
             *
             * data[0] contains a ID3D11Texture2D pointer, and data[1] contains the
             * texture array index of the frame as intptr_t if the ID3D11Texture2D is
             * an array texture (or always 0 if it's a normal texture).
             */
            AV_PIX_FMT_D3D11,

            AV_PIX_FMT_GRAY9BE,         //<        Y        , 9bpp, big-endian
            AV_PIX_FMT_GRAY9LE,         //<        Y        , 9bpp, little-endian

            AV_PIX_FMT_NB               //< number of pixel formats, DO NOT USE THIS if you want to link with shared libav* because the number of formats might differ between versions
        };

        /**
        * Chromaticity coordinates of the source primaries.
        * These values match the ones defined by ISO/IEC 23001-8_2013 ยง 7.1.
        */
        public enum AVColorPrimaries
        {
            AVCOL_PRI_RESERVED0 = 0,
            AVCOL_PRI_BT709 = 1,        //< also ITU-R BT1361 / IEC 61966-2-4 / SMPTE RP177 Annex B
            AVCOL_PRI_UNSPECIFIED = 2,
            AVCOL_PRI_RESERVED = 3,
            AVCOL_PRI_BT470M = 4,       //< also FCC Title 47 Code of Federal Regulations 73.682 (a)(20)

            AVCOL_PRI_BT470BG = 5,      //< also ITU-R BT601-6 625 / ITU-R BT1358 625 / ITU-R BT1700 625 PAL & SECAM
            AVCOL_PRI_SMPTE170M = 6,    //< also ITU-R BT601-6 525 / ITU-R BT1358 525 / ITU-R BT1700 NTSC
            AVCOL_PRI_SMPTE240M = 7,    //< functionally identical to above
            AVCOL_PRI_FILM = 8,         //< colour filters using Illuminant C
            AVCOL_PRI_BT2020 = 9,       //< ITU-R BT2020
            AVCOL_PRI_SMPTE428 = 10,    //< SMPTE ST 428-1 (CIE 1931 XYZ)
            AVCOL_PRI_SMPTEST428_1 = AVCOL_PRI_SMPTE428,
            AVCOL_PRI_SMPTE431 = 11,    //< SMPTE ST 431-2 (2011) / DCI P3
            AVCOL_PRI_SMPTE432 = 12,    //< SMPTE ST 432-1 (2010) / P3 D65 / Display P3
            AVCOL_PRI_JEDEC_P22 = 22,   //< JEDEC P22 phosphors
            AVCOL_PRI_NB                //< Not part of ABI
        };

        /**
        * Color Transfer Characteristic.
        * These values match the ones defined by ISO/IEC 23001-8_2013 ยง 7.2.
        */
        public enum AVColorTransferCharacteristic
        {
            AVCOL_TRC_RESERVED0 = 0,
            AVCOL_TRC_BT709 = 1,            //< also ITU-R BT1361
            AVCOL_TRC_UNSPECIFIED = 2,
            AVCOL_TRC_RESERVED = 3,
            AVCOL_TRC_GAMMA22 = 4,          //< also ITU-R BT470M / ITU-R BT1700 625 PAL & SECAM
            AVCOL_TRC_GAMMA28 = 5,          //< also ITU-R BT470BG
            AVCOL_TRC_SMPTE170M = 6,        //< also ITU-R BT601-6 525 or 625 / ITU-R BT1358 525 or 625 / ITU-R BT1700 NTSC
            AVCOL_TRC_SMPTE240M = 7,
            AVCOL_TRC_LINEAR = 8,           //< "Linear transfer characteristics"
            AVCOL_TRC_LOG = 9,              //< "Logarithmic transfer characteristic (100:1 range)"
            AVCOL_TRC_LOG_SQRT = 10,        //< "Logarithmic transfer characteristic (100 * Sqrt(10) : 1 range)"
            AVCOL_TRC_IEC61966_2_4 = 11,    //< IEC 61966-2-4
            AVCOL_TRC_BT1361_ECG = 12,      //< ITU-R BT1361 Extended Colour Gamut
            AVCOL_TRC_IEC61966_2_1 = 13,    //< IEC 61966-2-1 (sRGB or sYCC)
            AVCOL_TRC_BT2020_10 = 14,       //< ITU-R BT2020 for 10-bit system
            AVCOL_TRC_BT2020_12 = 15,       //< ITU-R BT2020 for 12-bit system
            AVCOL_TRC_SMPTE2084 = 16,       //< SMPTE ST 2084 for 10-, 12-, 14- and 16-bit systems
            AVCOL_TRC_SMPTEST2084 = AVCOL_TRC_SMPTE2084,
            AVCOL_TRC_SMPTE428 = 17,        //< SMPTE ST 428-1
            AVCOL_TRC_SMPTEST428_1 = AVCOL_TRC_SMPTE428,
            AVCOL_TRC_ARIB_STD_B67 = 18,    //< ARIB STD-B67, known as "Hybrid log-gamma"
            AVCOL_TRC_NB                    //< Not part of ABI
        };

        /**
         * YUV colorspace type.
         * These values match the ones defined by ISO/IEC 23001-8_2013 ยง 7.3.
         */
        public enum AVColorSpace
        {
            AVCOL_SPC_RGB = 0,          //< order of coefficients is actually GBR, also IEC 61966-2-1 (sRGB)
            AVCOL_SPC_BT709 = 1,        //< also ITU-R BT1361 / IEC 61966-2-4 xvYCC709 / SMPTE RP177 Annex B
            AVCOL_SPC_UNSPECIFIED = 2,
            AVCOL_SPC_RESERVED = 3,
            AVCOL_SPC_FCC = 4,          //< FCC Title 47 Code of Federal Regulations 73.682 (a)(20)
            AVCOL_SPC_BT470BG = 5,      //< also ITU-R BT601-6 625 / ITU-R BT1358 625 / ITU-R BT1700 625 PAL & SECAM / IEC 61966-2-4 xvYCC601
            AVCOL_SPC_SMPTE170M = 6,    //< also ITU-R BT601-6 525 / ITU-R BT1358 525 / ITU-R BT1700 NTSC
            AVCOL_SPC_SMPTE240M = 7,    //< functionally identical to above
            AVCOL_SPC_YCGCO = 8,        //< Used by Dirac / VC-2 and H.264 FRext, see ITU-T SG16
            AVCOL_SPC_YCOCG = AVCOL_SPC_YCGCO,
            AVCOL_SPC_BT2020_NCL = 9,   //< ITU-R BT2020 non-constant luminance system
            AVCOL_SPC_BT2020_CL = 10,   //< ITU-R BT2020 constant luminance system
            AVCOL_SPC_SMPTE2085 = 11,   //< SMPTE 2085, Y'D'zD'x
            AVCOL_SPC_NB                //< Not part of ABI
        };

        /**
         * MPEG vs JPEG YUV range.
         */
        public enum AVColorRange
        {
            AVCOL_RANGE_UNSPECIFIED = 0,
            AVCOL_RANGE_MPEG = 1,       //< the normal 219*2^(n-8) "MPEG" YUV ranges
            AVCOL_RANGE_JPEG = 2,       //< the normal     2^n-1   "JPEG" YUV ranges
            AVCOL_RANGE_NB              //< Not part of ABI
        };

        /**
         * Location of chroma samples.
         *
         * Illustration showing the location of the first (top left) chroma sample of the
         * image, the left shows only luma, the right
         * shows the location of the chroma sample, the 2 could be imagined to overlay
         * each other but are drawn separately due to limitations of ASCII
         *
         *                1st 2nd       1st 2nd horizontal luma sample positions
         *                 v   v         v   v
         *                 ______        ______
         *1st luma line > |X   X ...    |3 4 X ...     X are luma samples,
         *                |             |1 2           1-6 are possible chroma positions
         *2nd luma line > |X   X ...    |5 6 X ...     0 is undefined/unknown position
         */
        public enum AVChromaLocation
        {
            AVCHROMA_LOC_UNSPECIFIED = 0,
            AVCHROMA_LOC_LEFT = 1,      //< MPEG-2/4 4:2:0, H.264 default for 4:2:0
            AVCHROMA_LOC_CENTER = 2,    //< MPEG-1 4:2:0, JPEG 4:2:0, H.263 4:2:0
            AVCHROMA_LOC_TOPLEFT = 3,   //< ITU-R 601, SMPTE 274M 296M S314M(DV 4:1:1), mpeg2 4:2:2
            AVCHROMA_LOC_TOP = 4,
            AVCHROMA_LOC_BOTTOMLEFT = 5,
            AVCHROMA_LOC_BOTTOM = 6,
            AVCHROMA_LOC_NB             //< Not part of ABI
        };

        public enum AVFieldOrder
        {
            AV_FIELD_UNKNOWN,
            AV_FIELD_PROGRESSIVE,
            AV_FIELD_TT,          //< Top coded_first, top displayed first
            AV_FIELD_BB,          //< Bottom coded first, bottom displayed first
            AV_FIELD_TB,          //< Top coded first, bottom displayed first
            AV_FIELD_BT,          //< Bottom coded first, top displayed first
        };

        public enum AVAudioServiceType
        {
            AV_AUDIO_SERVICE_TYPE_MAIN = 0,
            AV_AUDIO_SERVICE_TYPE_EFFECTS = 1,
            AV_AUDIO_SERVICE_TYPE_VISUALLY_IMPAIRED = 2,
            AV_AUDIO_SERVICE_TYPE_HEARING_IMPAIRED = 3,
            AV_AUDIO_SERVICE_TYPE_DIALOGUE = 4,
            AV_AUDIO_SERVICE_TYPE_COMMENTARY = 5,
            AV_AUDIO_SERVICE_TYPE_EMERGENCY = 6,
            AV_AUDIO_SERVICE_TYPE_VOICE_OVER = 7,
            AV_AUDIO_SERVICE_TYPE_KARAOKE = 8,
            AV_AUDIO_SERVICE_TYPE_NB, //< Not part of ABI
        };

        /**
        * Audio sample formats
        *
        * - The data described by the sample format is always in native-endian order.
        *   Sample values can be expressed by native C types, hence the lack of a signed
        *   24-bit sample format even though it is a common raw audio data format.
        *
        * - The floating-point formats are based on full volume being in the range
        *   [-1.0, 1.0]. Any values outside this range are beyond full volume level.
        *
        * - The data layout as used in av_samples_fill_arrays() and elsewhere in FFmpeg
        *   (such as AVFrame in libavcodec) is as follows:
        *
        * @par
        * For planar sample formats, each audio channel is in a separate data plane,
        * and linesize is the buffer size, in bytes, for a single plane. All data
        * planes must be the same size. For packed sample formats, only the first data
        * plane is used, and samples for each channel are interleaved. In this case,
        * linesize is the buffer size, in bytes, for the 1 plane.
        *
        */
        public enum AVSampleFormat
        {
            AV_SAMPLE_FMT_NONE = -1,
            AV_SAMPLE_FMT_U8,          //< unsigned 8 bits
            AV_SAMPLE_FMT_S16,         //< signed 16 bits
            AV_SAMPLE_FMT_S32,         //< signed 32 bits
            AV_SAMPLE_FMT_FLT,         //< float
            AV_SAMPLE_FMT_DBL,         //< double

            AV_SAMPLE_FMT_U8P,         //< unsigned 8 bits, planar
            AV_SAMPLE_FMT_S16P,        //< signed 16 bits, planar
            AV_SAMPLE_FMT_S32P,        //< signed 32 bits, planar
            AV_SAMPLE_FMT_FLTP,        //< float, planar
            AV_SAMPLE_FMT_DBLP,        //< double, planar
            AV_SAMPLE_FMT_S64,         //< signed 64 bits
            AV_SAMPLE_FMT_S64P,        //< signed 64 bits, planar

            AV_SAMPLE_FMT_NB           //< Number of sample formats. DO NOT USE if linking dynamically
        };

        public enum AVMediaType
        {
            AVMEDIA_TYPE_UNKNOWN = -1,  //< Usually treated as AVMEDIA_TYPE_DATA
            AVMEDIA_TYPE_VIDEO,
            AVMEDIA_TYPE_AUDIO,
            AVMEDIA_TYPE_DATA,          //< Opaque data information usually continuous
            AVMEDIA_TYPE_SUBTITLE,
            AVMEDIA_TYPE_ATTACHMENT,    //< Opaque data information usually sparse
            AVMEDIA_TYPE_NB
        };

        // AVPicture types, pixel formats and basic image planes manipulation.
        public enum AVPictureType
        {
            AV_PICTURE_TYPE_NONE = 0, ///< Undefined
            AV_PICTURE_TYPE_I,     ///< Intra
            AV_PICTURE_TYPE_P,     ///< Predicted
            AV_PICTURE_TYPE_B,     ///< Bi-dir predicted
            AV_PICTURE_TYPE_S,     ///< S(GMC)-VOP MPEG-4
            AV_PICTURE_TYPE_SI,    ///< Switching Intra
            AV_PICTURE_TYPE_SP,    ///< Switching Predicted
            AV_PICTURE_TYPE_BI,    ///< BI type
        };

        [StructLayout(LayoutKind.Sequential)]
        public class ByteIOContext
        {
            IntPtr buffer;

            [MarshalAs(UnmanagedType.I4)]
            int buffer_size;

            IntPtr buf_ptr;

            IntPtr buf_end;

            //[MarshalAs(UnmanagedType.FunctionPtr)]
            //AnonymousCallback opaque;
            IntPtr opaque;

            [MarshalAs(UnmanagedType.FunctionPtr)]
            Read_PacketCallback read_packet;

            [MarshalAs(UnmanagedType.FunctionPtr)]
            WritePacketCallback write_packet;

            [MarshalAs(UnmanagedType.FunctionPtr)]
            SeekCallback seek;

            [MarshalAs(UnmanagedType.I8)]
            Int64 pos; // position in the file of the current buffer 

            [MarshalAs(UnmanagedType.I4)]
            int must_flush; // true if the next seek should flush

            [MarshalAs(UnmanagedType.I4)]
            int eof_reached; // true if eof reached

            [MarshalAs(UnmanagedType.I4)]
            int write_flag;  // true if open for writing 

            [MarshalAs(UnmanagedType.I4)]
            int is_streamed;

            [MarshalAs(UnmanagedType.I4)]
            int max_packet_size;

            [MarshalAs(UnmanagedType.U4)]
            uint checksum;

            IntPtr checksum_ptr;

            [MarshalAs(UnmanagedType.FunctionPtr)]
            UpdateChecksumCallback update_checksum;

            [MarshalAs(UnmanagedType.I4)]
            int error; // contains the error code or 0 if no error happened
        };

        public delegate String ItemNameCallback();

        [StructLayout(LayoutKind.Sequential)]
        public struct AVClass
        {
            [MarshalAs(UnmanagedType.LPStr)]
            String class_name;
            ItemNameCallback item_name;
            IntPtr pAVOption;
        };

        public struct AVOption
        {
        };

        [StructLayout(LayoutKind.Sequential)]
        public struct AVRational
        {
            [MarshalAs(UnmanagedType.I4)]
            public int num;
            [MarshalAs(UnmanagedType.I4)]
            public int den;
        };
    }
}
