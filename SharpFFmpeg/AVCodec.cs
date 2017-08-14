using System;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Security;

namespace SharpFFmpeg
{
    public partial class FFmpeg
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="output_channels"></param>
        /// <param name="input_channels"></param>
        /// <param name="output_rate"></param>
        /// <param name="input_rate"></param>
        /// <returns>ReSampleContext pointer</returns>
        [DllImport("avcodec-57.dll"), SuppressUnmanagedCodeSecurity]
        public static extern IntPtr audio_resample_init(int output_channels, int input_channels,
                                                    int output_rate, int input_rate);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pResampleContext"></param>
        /// <param name="output"></param>
        /// <param name="intput"></param>
        /// <returns></returns>
        [DllImport("avcodec-57.dll"), SuppressUnmanagedCodeSecurity]
        public static extern int audio_resample(IntPtr pResampleContext, IntPtr output, IntPtr intput, int nb_samples);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pResampleContext"></param>
        [DllImport("avcodec-57.dll"), SuppressUnmanagedCodeSecurity]
        public static extern void audio_resample_close(IntPtr pResampleContext);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="out_rate"></param>
        /// <param name="in_rate"></param>
        /// <param name="filter_length"></param>
        /// <param name="log2_phase_count"></param>
        /// <param name="linear"></param>
        /// <param name="cutoff"></param>
        /// <returns>AVResampleContext pointer</returns>
        [DllImport("avcodec-57.dll"), SuppressUnmanagedCodeSecurity]
        public static extern IntPtr av_resample_init(int out_rate, int in_rate, int filter_length, int log2_phase_count, int linear, double cutoff);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pAVResampleContext"></param>
        /// <param name="dst"></param>
        /// <param name="src"></param>
        /// <param name="consumed"></param>
        /// <param name="src_size"></param>
        /// <param name="udpate_ctx"></param>
        /// <returns></returns>
        [DllImport("avcodec-57.dll"), SuppressUnmanagedCodeSecurity]
        public static extern int av_resample(IntPtr pAVResampleContext, IntPtr dst, IntPtr src, IntPtr consumed, int src_size, int udpate_ctx);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pAVResampleContext"></param>
        /// <param name="sample_delta"></param>
        /// <param name="compensation_distance"></param>
        [DllImport("avcodec-57.dll"), SuppressUnmanagedCodeSecurity]
        public static extern void av_resample_compensate(IntPtr pAVResampleContext, int sample_delta, int compensation_distance);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pAVResampleContext"></param>
        [DllImport("avcodec-57.dll"), SuppressUnmanagedCodeSecurity]
        public static extern void av_resample_close(IntPtr pAVResampleContext);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="output_width"></param>
        /// <param name="output_height"></param>
        /// <param name="input_width"></param>
        /// <param name="input_height"></param>
        /// <returns>ImgReSampleContext pointer</returns>
        [DllImport("avcodec-57.dll"), SuppressUnmanagedCodeSecurity]
        public static extern IntPtr img_resample_init(int output_width, int output_height,
                                      int input_width, int input_height);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="owidth"></param>
        /// <param name="oheight"></param>
        /// <param name="iwidth"></param>
        /// <param name="iheight"></param>
        /// <param name="topBand"></param>
        /// <param name="bottomBand"></param>
        /// <param name="leftBand"></param>
        /// <param name="rightBand"></param>
        /// <param name="padtop"></param>
        /// <param name="padbottom"></param>
        /// <param name="padleft"></param>
        /// <param name="padright"></param>
        /// <returns>ImgReSampleContext pointer</returns>
        [DllImport("avcodec-57.dll"), SuppressUnmanagedCodeSecurity]
        public static extern IntPtr img_resample_full_init(int owidth, int oheight,
                                      int iwidth, int iheight,
                                      int topBand, int bottomBand,
                                      int leftBand, int rightBand,
                                      int padtop, int padbottom,
                                      int padleft, int padright);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pImgReSampleContext"></param>
        /// <param name="p_output_AVPicture"></param>
        /// <param name="p_input_AVPicture"></param>
        [DllImport("avcodec-57.dll"), SuppressUnmanagedCodeSecurity]
        public static extern void img_resample(IntPtr pImgReSampleContext, IntPtr p_output_AVPicture, IntPtr p_input_AVPicture);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pImgReSampleContext"></param>
        [DllImport("avcodec-57.dll"), SuppressUnmanagedCodeSecurity]
        public static extern void ImgReSampleContext(IntPtr pImgReSampleContext);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pAVPicture"></param>
        /// <param name="pix_fmt"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>        
        [DllImport("avcodec-57.dll"), SuppressUnmanagedCodeSecurity]
        public static extern int avpicture_alloc(IntPtr pAVPicture, int pix_fmt, int width, int height);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pAVPicture"></param>
        [DllImport("avcodec-57.dll"), SuppressUnmanagedCodeSecurity]
        public static extern void avpicture_free(IntPtr pAVPicture);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pAVPicture"></param>
        /// <param name="ptr"></param>
        /// <param name="pix_fmt"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        [DllImport("avcodec-57.dll"), SuppressUnmanagedCodeSecurity]
        public static extern int avpicture_fill(IntPtr pAVPicture, IntPtr ptr, int pix_fmt, int width, int height);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_src_AVPicture"></param>
        /// <param name="pix_fmt"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="dest"></param>
        /// <param name="dest_size"></param>
        /// <returns></returns>
        [DllImport("avcodec-57.dll"), SuppressUnmanagedCodeSecurity]
        public static extern int avpicture_layout(IntPtr p_src_AVPicture, int pix_fmt, int width, int height,
                                           IntPtr dest, int dest_size);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pix_fmt"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        [DllImport("avcodec-57.dll"), SuppressUnmanagedCodeSecurity]
        public static extern int avpicture_get_size(int pix_fmt, int width, int height);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pix_fmt"></param>
        /// <param name="h_shift"></param>
        /// <param name="v_shift"></param>
        [DllImport("avcodec-57.dll"), SuppressUnmanagedCodeSecurity]
        public static extern void avcodec_get_chroma_sub_sample(int pix_fmt, IntPtr h_shift, IntPtr v_shift);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pix_fmt"></param>
        /// <returns></returns>
        [DllImport("avcodec-57.dll"), SuppressUnmanagedCodeSecurity]
        public static extern String avcodec_get_pix_fmt_name(int pix_fmt);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pAVCodecContext"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        [DllImport("avcodec-57.dll"), SuppressUnmanagedCodeSecurity]
        public static extern void avcodec_set_dimensions(AVCodecContext pAVCodecContext, int width, int height);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [DllImport("avcodec-57.dll"), SuppressUnmanagedCodeSecurity]
        public static extern AVPixelFormat avcodec_get_pix_fmt([MarshalAs(UnmanagedType.LPStr)]String name);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        [DllImport("avcodec-57.dll"), SuppressUnmanagedCodeSecurity]
        public static extern uint avcodec_pix_fmt_to_codec_tag(AVPixelFormat p);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dst_pix_fmt"></param>
        /// <param name="src_pix_fmt"></param>
        /// <param name="has_alpha"></param>
        /// <returns></returns>
        [DllImport("avcodec-57.dll"), SuppressUnmanagedCodeSecurity]
        public static extern int avcodec_get_pix_fmt_loss(int dst_pix_fmt, int src_pix_fmt, int has_alpha);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pix_fmt_mask"></param>
        /// <param name="src_pix_fmt"></param>
        /// <param name="has_alpha"></param>
        /// <param name="loss_ptr"></param>
        /// <returns></returns>
        [DllImport("avcodec-57.dll"), SuppressUnmanagedCodeSecurity]
        public static extern int avcodec_find_best_pix_fmt(int pix_fmt_mask, int src_pix_fmt, int has_alpha, IntPtr loss_ptr);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pAVPicture"></param>
        /// <param name="pix_fmt"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        [DllImport("avcodec-57.dll"), SuppressUnmanagedCodeSecurity]
        public static extern int img_get_alpha_info(IntPtr pAVPicture, int pix_fmt, int width, int height);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_dst_AVPicture"></param>
        /// <param name="dst_pix_fmt"></param>
        /// <param name="p_src_AVPicture"></param>
        /// <param name="pix_fmt"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        [DllImport("avcodec-57.dll"), SuppressUnmanagedCodeSecurity]
        public static extern int img_convert(IntPtr p_dst_AVPicture, int dst_pix_fmt,
                            IntPtr p_src_AVPicture, int pix_fmt, int width, int height);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_dst_AVPicture"></param>
        /// <param name="p_src_AVPicture"></param>
        /// <param name="pix_fmt"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        [DllImport("avcodec-57.dll"), SuppressUnmanagedCodeSecurity]
        public static extern int avpicture_deinterlace(IntPtr p_dst_AVPicture, IntPtr p_src_AVPicture,
                            int pix_fmt, int width, int height);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [DllImport("avcodec-57.dll"), SuppressUnmanagedCodeSecurity]
        public static extern uint avcodec_version();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [DllImport("avcodec-57.dll"), SuppressUnmanagedCodeSecurity]
        public static extern uint avcodec_build();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [DllImport("avcodec-57.dll"), SuppressUnmanagedCodeSecurity]
        public static extern uint avcodec_init();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pAVCodec"></param>
        [DllImport("avcodec-57.dll"), SuppressUnmanagedCodeSecurity]
        public static extern void register_avcodec(IntPtr pAVCodec);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>AVCodec pointer</returns>
        [DllImport("avcodec-57.dll"), SuppressUnmanagedCodeSecurity]
        public static extern AVCodec avcodec_find_encoder(AVCodecID id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mame"></param>
        /// <returns>AVCodec pointer</returns>
        [DllImport("avcodec-57.dll"), SuppressUnmanagedCodeSecurity]
        public static extern IntPtr avcodec_find_encoder_by_name(
                    [MarshalAs(UnmanagedType.LPStr)]String mame);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>AVCodec pointer</returns>
        [DllImport("avcodec-57.dll"), SuppressUnmanagedCodeSecurity]
        public static extern IntPtr avcodec_find_decoder(AVCodecID id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mame"></param>
        /// <returns>AVCodec pointer</returns>
        [DllImport("avcodec-57.dll"), SuppressUnmanagedCodeSecurity]
        public static extern IntPtr avcodec_find_decoder_by_name(
                    [MarshalAs(UnmanagedType.LPStr)]String mame);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mam"></param>
        /// <param name="buf_size"></param>
        /// <param name="pAVCodeContext"></param>
        /// <param name="encode"></param>
        [DllImport("avcodec-57.dll"), SuppressUnmanagedCodeSecurity]
        public static extern void avcodec_string(
                    [MarshalAs(UnmanagedType.LPStr)]String mam, int buf_size,
                    IntPtr pAVCodeContext, int encode);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pAVCodecContext"></param>
        [DllImport("avcodec-57.dll"), SuppressUnmanagedCodeSecurity]
        public static extern int avcodec_get_context_defaults3(AVCodecContext pAVCodecContext);

        /// <summary>
        /// 
        /// </summary>
        /// <returns>AVCodecContext pointer</returns>
        [DllImport("avcodec-57.dll"), SuppressUnmanagedCodeSecurity]
        public static extern AVCodecContext avcodec_alloc_context3(AVCodec pAVCodec);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pAVFrame"></param>
        [DllImport("avcodec-57.dll"), SuppressUnmanagedCodeSecurity]
        public static extern void avcodec_get_frame_defaults(AVFrame pAVFrame);

        /// <summary>
        /// 
        /// </summary>
        /// <returns>AVFrame pointer</returns>
        [DllImport("avcodec-57.dll"), SuppressUnmanagedCodeSecurity]
        public static extern IntPtr avcodec_alloc_frame();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pAVCodecContext"></param>
        /// <param name="pAVFrame"></param>
        /// <returns></returns>
        [DllImport("avcodec-57.dll"), SuppressUnmanagedCodeSecurity]
        public static extern int avcodec_default_get_buffer2(AVCodecContext pAVCodecContext, AVFrame pAVFrame);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pAVCodecContext"></param>
        /// <param name="pAVFrame"></param>
        [DllImport("avcodec-57.dll"), SuppressUnmanagedCodeSecurity]
        public static extern void avcodec_default_release_buffer(AVCodecContext pAVCodecContext, AVFrame pAVFrame);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pAVCodecContext"></param>
        /// <param name="pAVFrame"></param>
        [DllImport("avcodec-57.dll"), SuppressUnmanagedCodeSecurity]
        public static extern void avcodec_default_reget_buffer(AVCodecContext pAVCodecContext, AVFrame pAVFrame);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pAVCodecContext"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        [DllImport("avcodec-57.dll"), SuppressUnmanagedCodeSecurity]
        public static extern void avcodec_align_dimensions(AVCodecContext pAVCodecContext, ref int width, ref int height);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="av_log_ctx"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        [DllImport("avcodec-57.dll"), SuppressUnmanagedCodeSecurity]
        public static extern int avcodec_check_dimensions(IntPtr av_log_ctx, ref uint width, ref uint height);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pAVCodecContext"></param>
        /// <param name="fmt"></param>
        /// <returns></returns>
        [DllImport("avcodec-57.dll"), SuppressUnmanagedCodeSecurity]
        public static extern AVPixelFormat avcodec_default_get_format(AVCodecContext pAVCodecContext, ref AVPixelFormat fmt);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pAVCodecContext"></param>
        /// <param name="thread_count"></param>
        /// <returns></returns>
        [DllImport("avcodec-57.dll"), SuppressUnmanagedCodeSecurity]
        public static extern int avcodec_thread_init(AVCodecContext pAVCodecContext, int thread_count);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pAVCodecContext"></param>
        [DllImport("avcodec-57.dll"), SuppressUnmanagedCodeSecurity]
        public static extern void avcodec_thread_free(AVCodecContext pAVCodecContext);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pAVCodecContext"></param>
        /// <param name="func"></param>
        /// <param name="arg"></param>
        /// <param name="ret"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [DllImport("avcodec-57.dll"), SuppressUnmanagedCodeSecurity]
        public static extern int avcodec_thread_execute(AVCodecContext pAVCodecContext,
                                [MarshalAs(UnmanagedType.FunctionPtr)]FuncCallback func,
                                [MarshalAs(UnmanagedType.LPArray)]IntPtr[] arg, ref int ret, int count);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pAVCodecContext"></param>
        /// <param name="func"></param>
        /// <param name="arg"></param>
        /// <param name="ret"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [DllImport("avcodec-57.dll"), SuppressUnmanagedCodeSecurity]
        public static extern int avcodec_default_execute(AVCodecContext pAVCodecContext,
                               [MarshalAs(UnmanagedType.FunctionPtr)]FuncCallback func,
                               [MarshalAs(UnmanagedType.LPArray)]IntPtr[] arg, ref int ret, int count);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pAVCodecContext"></param>
        /// <param name="pAVCodec"></param>
        /// <returns></returns>
        [DllImport("avcodec-57.dll"), SuppressUnmanagedCodeSecurity]
        public static extern int avcodec_open2(AVCodecContext pAVCodecContext, AVCodec pAVCodec, IntPtr pAVDictionary);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pAVCodecContext"></param>
        /// <param name="samples"></param>
        /// <param name="frame_size_ptr"></param>
        /// <param name="buf"></param>
        /// <param name="buf_size"></param>
        /// <returns></returns>
        [DllImport("avcodec-57.dll"), SuppressUnmanagedCodeSecurity]
        public static extern int avcodec_decode_audio4(AVCodecContext pAVCodecContext,
                                            IntPtr samples, out int frame_size_ptr,
                                            IntPtr buf, int buf_size);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pAVCodecContext"></param>
        /// <param name="pAVFrame"></param>
        /// <param name="got_picture_ptr"></param>
        /// <param name="buf"></param>
        /// <param name="buf_size"></param>
        /// <returns></returns>
        [DllImport("avcodec-57.dll"), SuppressUnmanagedCodeSecurity]
        public static extern int avcodec_decode_video2(AVCodecContext pAVCodecContext, AVFrame pAVFrame,
                                            ref int got_picture_ptr, IntPtr buf, int buf_size);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pAVCodecContext"></param>
        /// <param name="pAVSubtitle"></param>
        /// <param name="got_sub_ptr"></param>
        /// <param name="buf"></param>
        /// <param name="buf_size"></param>
        /// <returns></returns>
        [DllImport("avcodec-57.dll"), SuppressUnmanagedCodeSecurity]
        public static extern int avcodec_decode_subtitle2(AVCodecContext pAVCodecContext, IntPtr pAVSubtitle,
                                           ref int got_sub_ptr, IntPtr buf, int buf_size);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pAVCodecContext"></param>
        /// <param name="pdata"></param>
        /// <param name="data_size_ptr"></param>
        /// <param name="buf"></param>
        /// <param name="buf_size"></param>
        /// <returns></returns>
        [DllImport("avcodec-57.dll"), SuppressUnmanagedCodeSecurity]
        public static extern int avcodec_parse_frame(AVCodecContext pAVCodecContext,
                                            [MarshalAs(UnmanagedType.LPArray)]IntPtr[] pdata,
                                            IntPtr data_size_ptr, IntPtr buf, int buf_size);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="pAVCodecContext"></param>
        /// <param name="buf"></param>
        /// <param name="buf_size"></param>
        /// <param name="pAVSubtitle"></param>
        /// <returns></returns>
        [DllImport("avcodec-57.dll"), SuppressUnmanagedCodeSecurity]
        public static extern int avcodec_encode_subtitle(AVCodecContext pAVCodecContext, IntPtr buf, int buf_size,
                                            IntPtr pAVSubtitle);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pAVCodecContext"></param>
        /// <returns></returns>
        [DllImport("avcodec-57.dll"), SuppressUnmanagedCodeSecurity]
        public static extern int avcodec_close(AVCodecContext pAVCodecContext);

        /// <summary>
        /// 
        /// </summary>
        [DllImport("avcodec-57.dll"), SuppressUnmanagedCodeSecurity]
        public static extern void avcodec_register_all();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pAVCodecContext"></param>
        [DllImport("avcodec-57.dll"), SuppressUnmanagedCodeSecurity]
        public static extern void avcodec_flush_buffers(AVCodecContext pAVCodecContext);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pAVCodecContext"></param>
        [DllImport("avcodec-57.dll"), SuppressUnmanagedCodeSecurity]
        public static extern void avcodec_default_free_buffers(AVCodecContext pAVCodecContext);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pict_type"></param>
        /// <returns></returns>
        [DllImport("avcodec-57.dll"), SuppressUnmanagedCodeSecurity]
        public static extern byte av_get_pict_type_char(int pict_type);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="codec_id"></param>
        /// <returns></returns>
        [DllImport("avcodec-57.dll"), SuppressUnmanagedCodeSecurity]
        public static extern int av_get_bits_per_sample(AVCodecID codec_id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pAVcodecParser"></param>
        [DllImport("avcodec-57.dll"), SuppressUnmanagedCodeSecurity]
        public static extern void av_register_codec_parser(IntPtr pAVcodecParser);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="codec_id"></param>
        /// <returns>AVCodecParserContext pointer</returns>
        [DllImport("avcodec-57.dll"), SuppressUnmanagedCodeSecurity]
        public static extern IntPtr av_parser_init(int codec_id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pAVCodecParserContext"></param>
        /// <param name="pAVCodecContext"></param>
        /// <param name="poutbuf"></param>
        /// <param name="poutbuf_size"></param>
        /// <param name="buf"></param>
        /// <param name="buf_size"></param>
        /// <param name="pts"></param>
        /// <param name="dts"></param>
        /// <returns></returns>
        [DllImport("avcodec-57.dll"), SuppressUnmanagedCodeSecurity]
        public static extern int av_parser_parse2(IntPtr pAVCodecParserContext,
                                                AVCodecContext pAVCodecContext,
                            [MarshalAs(UnmanagedType.LPArray)]IntPtr[] poutbuf, ref int poutbuf_size,
                            IntPtr buf, int buf_size, Int64 pts, Int64 dts);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pAVCodecParserContext"></param>
        /// <param name="pAVCodecContext"></param>
        /// <param name="poutbuf"></param>
        /// <param name="poutbuf_size"></param>
        /// <param name="buf"></param>
        /// <param name="buf_size"></param>
        /// <param name="keyframe"></param>
        /// <returns></returns>
        [DllImport("avcodec-57.dll"), SuppressUnmanagedCodeSecurity]
        public static extern int av_parser_change(IntPtr pAVCodecParserContext,
                                                AVCodecContext pAVCodecContext,
                            [MarshalAs(UnmanagedType.LPArray)]IntPtr[] poutbuf, ref int poutbuf_size,
                            IntPtr buf, int buf_size, int keyframe);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pAVCodecParserContext"></param>
        [DllImport("avcodec-57.dll"), SuppressUnmanagedCodeSecurity]
        public static extern void av_parser_close(IntPtr pAVCodecParserContext);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pAVBitStreamFilter"></param>
        [DllImport("avcodec-57.dll"), SuppressUnmanagedCodeSecurity]
        public static extern void av_register_bitstream_filter(IntPtr pAVBitStreamFilter);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns>AVBitStreamFilterContext pointer</returns>
        [DllImport("avcodec-57.dll"), SuppressUnmanagedCodeSecurity]
        public static extern IntPtr av_bitstream_filter_init([MarshalAs(UnmanagedType.LPStr)]
                                        String name);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pAVBitStreamFilterContext"></param>
        /// <param name="pAVCodecContext"></param>
        /// <param name="args"></param>
        /// <param name="poutbuf"></param>
        /// <param name="poutbuf_size"></param>
        /// <param name="buf"></param>
        /// <param name="buf_size"></param>
        /// <param name="keyframe"></param>
        /// <returns></returns>
        [DllImport("avcodec-57.dll"), SuppressUnmanagedCodeSecurity]
        public static extern int av_bitstream_filter_filter(IntPtr pAVBitStreamFilterContext,
                                        AVCodecContext pAVCodecContext,
                                        [MarshalAs(UnmanagedType.LPStr)]String args,
                                        [MarshalAs(UnmanagedType.LPArray)]IntPtr[] poutbuf,
                                        ref  int poutbuf_size, IntPtr buf, int buf_size, int keyframe);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pAVBitStreamFilterContext"></param>
        [DllImport("avcodec-57.dll"), SuppressUnmanagedCodeSecurity]
        public static extern void av_bitstream_filter_close(IntPtr pAVBitStreamFilterContext);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="size"></param>
        [DllImport("avcodec-57.dll"), SuppressUnmanagedCodeSecurity]
        public static extern void av_mallocz(uint size);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        [DllImport("avcodec-57.dll"), SuppressUnmanagedCodeSecurity]
        public static extern IntPtr av_strdup([MarshalAs(UnmanagedType.LPStr)]String s);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ptr"></param>
        [DllImport("avcodec-57.dll"), SuppressUnmanagedCodeSecurity]
        public static extern void av_freep(IntPtr ptr);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ptr"></param>
        /// <param name="size"></param>
        /// <param name="min_size"></param>
        [DllImport("avcodec-57.dll"), SuppressUnmanagedCodeSecurity]
        public static extern void av_fast_realloc(IntPtr ptr, ref uint size, ref uint min_size);

        /// <summary>
        /// 
        /// </summary>
        [DllImport("avcodec-57.dll"), SuppressUnmanagedCodeSecurity]
        public static extern void av_free_static();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="size"></param>
        [DllImport("avcodec-57.dll"), SuppressUnmanagedCodeSecurity]
        public static extern void av_mallocz_static(uint size);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ptr"></param>
        /// <param name="size"></param>
        [DllImport("avcodec-57.dll"), SuppressUnmanagedCodeSecurity]
        public static extern void av_realloc_static(IntPtr ptr, uint size);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pAVPicture"></param>
        /// <param name="p_src_AVPicture"></param>
        /// <param name="pix_fmt"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        [DllImport("avcodec-57.dll"), SuppressUnmanagedCodeSecurity]
        public static extern void img_copy(IntPtr pAVPicture, IntPtr p_src_AVPicture,
                            int pix_fmt, int width, int height);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_dst_pAVPicture"></param>
        /// <param name="p_src_pAVPicture"></param>
        /// <param name="pix_fmt"></param>
        /// <param name="top_band"></param>
        /// <param name="left_band"></param>
        /// <returns></returns>
        [DllImport("avcodec-57.dll"), SuppressUnmanagedCodeSecurity]
        public static extern int img_crop(IntPtr p_dst_pAVPicture, IntPtr p_src_pAVPicture,
                            int pix_fmt, int top_band, int left_band);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_dst_pAVPicture"></param>
        /// <param name="p_src_pAVPicture"></param>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <param name="pix_fmt"></param>
        /// <param name="padtop"></param>
        /// <param name="padbottom"></param>
        /// <param name="padleft"></param>
        /// <param name="padright"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        [DllImport("avcodec-57.dll"), SuppressUnmanagedCodeSecurity]
        public static extern int img_pad(IntPtr p_dst_pAVPicture, IntPtr p_src_pAVPicture,
                            int height, int width, int pix_fmt, int padtop, int padbottom,
                            int padleft, int padright, ref int color);

        [DllImport("avcodec-57.dll"), SuppressUnmanagedCodeSecurity]
        public static extern int avcodec_send_frame(AVCodecContext pAVCodecContext, AVFrame pAVFrame);

        [DllImport("avcodec-57.dll"), SuppressUnmanagedCodeSecurity]
        public static extern int avcodec_receive_packet(AVCodecContext pAVCodecContext, AVPacket pAVPacket);

        [DllImport("avcodec-57.dll"), SuppressUnmanagedCodeSecurity]
        public static extern AVPacket av_packet_alloc();

        [DllImport("avcodec-57.dll"), SuppressUnmanagedCodeSecurity]
        public static extern void avcodec_free_context([In, Out] AVCodecContext pAVCodecContext);

        [DllImport("avcodec-57.dll"), SuppressUnmanagedCodeSecurity]
        public static extern void av_packet_free([In,Out] AVPacket pAVPacket);

        // *********************************************************************************
        // Constants
        // *********************************************************************************

        // 1 second of 48khz 32bit audio in bytes
        public const int AVCODEC_MAX_AUDIO_FRAME_SIZE = 192000;

        /**
      * Required number of additionally allocated bytes at the end of the input bitstream for decoding.
      * this is mainly needed because some optimized bitstream readers read
      * 32 or 64 bit at once and could read over the end<br>
      * Note, if the first 23 bits of the additional bytes are not 0 then damaged
      * MPEG bitstreams could cause overread and segfault
      */
        public const int FF_INPUT_BUFFER_PADDING_SIZE = 8;

        /**
         * minimum encoding buffer size.
         * used to avoid some checks during header writing
         */
        public const int FF_MIN_BUFFER_SIZE = 16384;

        public const int FF_MAX_B_FRAMES = 16;

        /* encoding support
           these flags can be passed in AVCodecContext.flags before initing
           Note: not everything is supported yet.
        */

        public const int CODEC_FLAG_QSCALE = 0x0002;  // use fixed qscale
        public const int CODEC_FLAG_4MV = 0x0004;  // 4 MV per MB allowed / Advanced prediction for H263
        public const int CODEC_FLAG_QPEL = 0x0010;  // use qpel MC
        public const int CODEC_FLAG_GMC = 0x0020;  // use GMC
        public const int CODEC_FLAG_MV0 = 0x0040;  // always try a MB with MV=<0,0>
        public const int CODEC_FLAG_PART = 0x0080;  // use data partitioning
        /* parent program gurantees that the input for b-frame containing streams is not written to
           for at least s->max_b_frames+1 frames, if this is not set than the input will be copied */
        public const int CODEC_FLAG_INPUT_PRESERVED = 0x0100;
        public const int CODEC_FLAG_PASS1 = 0x0200;   // use internal 2pass ratecontrol in first  pass mode
        public const int CODEC_FLAG_PASS2 = 0x0400;   // use internal 2pass ratecontrol in second pass mode
        public const int CODEC_FLAG_EXTERN_HUFF = 0x1000; // use external huffman table (for mjpeg)
        public const int CODEC_FLAG_GRAY = 0x2000;   // only decode/encode grayscale
        public const int CODEC_FLAG_EMU_EDGE = 0x4000;// don't draw edges
        public const int CODEC_FLAG_PSNR = 0x8000; // error[?] variables will be set during encoding
        public const int CODEC_FLAG_TRUNCATED = 0x00010000; /** input bitstream might be truncated at a random location instead
                                            of only at frame boundaries */
        public const int CODEC_FLAG_NORMALIZE_AQP = 0x00020000; // normalize adaptive quantization
        public const int CODEC_FLAG_INTERLACED_DCT = 0x00040000; // use interlaced dct
        public const int CODEC_FLAG_LOW_DELAY = 0x00080000; // force low delay
        public const int CODEC_FLAG_ALT_SCAN = 0x00100000; // use alternate scan
        public const int CODEC_FLAG_TRELLIS_QUANT = 0x00200000; // use trellis quantization
        public const int CODEC_FLAG_GLOBAL_HEADER = 0x00400000; // place global headers in extradata instead of every keyframe
        public const int CODEC_FLAG_BITEXACT = 0x00800000; // use only bitexact stuff (except (i)dct)
        /* Fx : Flag for h263+ extra options */
        public const int CODEC_FLAG_H263P_AIC = 0x01000000; // H263 Advanced intra coding / MPEG4 AC prediction (remove this)
        public const int CODEC_FLAG_AC_PRED = 0x01000000; // H263 Advanced intra coding / MPEG4 AC prediction
        public const int CODEC_FLAG_H263P_UMV = 0x02000000; // Unlimited motion vector
        public const int CODEC_FLAG_CBP_RD = 0x04000000; // use rate distortion optimization for cbp
        public const int CODEC_FLAG_QP_RD = 0x08000000; // use rate distortion optimization for qp selectioon
        public const int CODEC_FLAG_H263P_AIV = 0x00000008; // H263 Alternative inter vlc
        public const int CODEC_FLAG_OBMC = 0x00000001; // OBMC
        public const int CODEC_FLAG_LOOP_FILTER = 0x00000800; // loop filter
        public const int CODEC_FLAG_H263P_SLICE_STRUCT = 0x10000000;
        public const int CODEC_FLAG_INTERLACED_ME = 0x20000000; // interlaced motion estimation
        public const int CODEC_FLAG_SVCD_SCAN_OFFSET = 0x40000000; // will reserve space for SVCD scan offset user data
        public const uint CODEC_FLAG_CLOSED_GOP = ((uint)0x80000000);
        public const int CODEC_FLAG2_FAST = 0x00000001; // allow non spec compliant speedup tricks
        public const int CODEC_FLAG2_STRICT_GOP = 0x00000002; // strictly enforce GOP size
        public const int CODEC_FLAG2_NO_OUTPUT = 0x00000004; // skip bitstream encoding
        public const int CODEC_FLAG2_LOCAL_HEADER = 0x00000008; // place global headers at every keyframe instead of in extradata
        public const int CODEC_FLAG2_BPYRAMID = 0x00000010; // H.264 allow b-frames to be used as references
        public const int CODEC_FLAG2_WPRED = 0x00000020; // H.264 weighted biprediction for b-frames
        public const int CODEC_FLAG2_MIXED_REFS = 0x00000040; // H.264 multiple references per partition
        public const int CODEC_FLAG2_8X8DCT = 0x00000080; // H.264 high profile 8x8 transform
        public const int CODEC_FLAG2_FASTPSKIP = 0x00000100; // H.264 fast pskip
        public const int CODEC_FLAG2_AUD = 0x00000200; // H.264 access unit delimiters
        public const int CODEC_FLAG2_BRDO = 0x00000400; // b-frame rate-distortion optimization
        public const int CODEC_FLAG2_INTRA_VLC = 0x00000800; // use MPEG-2 intra VLC table
        public const int CODEC_FLAG2_MEMC_ONLY = 0x00001000; // only do ME/MC (I frames -> ref, P frame -> ME+MC)

        /* Unsupported options :
         * Syntax Arithmetic coding (SAC)
         * Reference Picture Selection
         * Independant Segment Decoding */
        /* /Fx */
        /* codec capabilities */
        public const int CODEC_CAP_DRAW_HORIZ_BAND = 0x0001; // decoder can use draw_horiz_band callback

        /**
         * Codec uses get_buffer() for allocating buffers.
         * direct rendering method 1
         */
        public const int CODEC_CAP_DR1 = 0x0002;

        /* if 'parse_only' field is true, then avcodec_parse_frame() can be
            used */
        public const int CODEC_CAP_PARSE_ONLY = 0x0004;
        public const int CODEC_CAP_TRUNCATED = 0x0008;

        /* codec can export data for HW decoding (XvMC) */
        public const int CODEC_CAP_HWACCEL = 0x0010;

        /**
         * codec has a non zero delay and needs to be feeded with NULL at the end to get the delayed data.
         * if this is not set, the codec is guranteed to never be feeded with NULL data
         */
        public const int CODEC_CAP_DELAY = 0x0020;

        /**
         * Codec can be fed a final frame with a smaller size.
         * This can be used to prevent truncation of the last audio samples.
        */
        public const int CODEC_CAP_SMALL_LAST_FRAME = 0x0040;

        //the following defines may change, don't expect compatibility if you use them
        public const int MB_TYPE_INTRA4x4 = 0x001;
        public const int MB_TYPE_INTRA16x16 = 0x0002; //FIXME h264 specific
        public const int MB_TYPE_INTRA_PCM = 0x0004; //FIXME h264 specific
        public const int MB_TYPE_16x16 = 0x0008;
        public const int MB_TYPE_16x8 = 0x0010;
        public const int MB_TYPE_8x16 = 0x0020;
        public const int MB_TYPE_8x8 = 0x0040;
        public const int MB_TYPE_INTERLACED = 0x0080;
        public const int MB_TYPE_DIRECT2 = 0x0100; //FIXME
        public const int MB_TYPE_ACPRED = 0x0200;
        public const int MB_TYPE_GMC = 0x0400;
        public const int MB_TYPE_SKIP = 0x0800;
        public const int MB_TYPE_P0L0 = 0x1000;
        public const int MB_TYPE_P1L0 = 0x2000;
        public const int MB_TYPE_P0L1 = 0x4000;
        public const int MB_TYPE_P1L1 = 0x8000;
        public const int MB_TYPE_L0 = (MB_TYPE_P0L0 | MB_TYPE_P1L0);
        public const int MB_TYPE_L1 = (MB_TYPE_P0L1 | MB_TYPE_P1L1);
        public const int MB_TYPE_L0L1 = (MB_TYPE_L0 | MB_TYPE_L1);
        public const int MB_TYPE_QUANT = 0x00010000;
        public const int MB_TYPE_CBP = 0x00020000;
        //Note bits 24-31 are reserved for codec specific use (h264 ref0, mpeg1 0mv, ...)

        public const int FF_QSCALE_TYPE_MPEG1 = 0;
        public const int FF_QSCALE_TYPE_MPEG2 = 1;
        public const int FF_QSCALE_TYPE_H264 = 2;

        public const int FF_BUFFER_TYPE_INTERNAL = 1;
        public const int FF_BUFFER_TYPE_USER = 2; // Direct rendering buffers (image is (de)allocated by user)
        public const int FF_BUFFER_TYPE_SHARED = 4; // buffer from somewhere else, don't dealloc image (data/base), all other tables are not shared
        public const int FF_BUFFER_TYPE_COPY = 8; // just a (modified) copy of some other buffer, don't dealloc anything

        public const int FF_I_TYPE = 1; // Intra
        public const int FF_P_TYPE = 2; // Predicted
        public const int FF_B_TYPE = 3; // Bi-dir predicted
        public const int FF_S_TYPE = 4; // S(GMC)-VOP MPEG4
        public const int FF_SI_TYPE = 5;
        public const int FF_SP_TYPE = 6;

        public const int FF_BUFFER_HINTS_VALID = 0x01; // Buffer hints value is meaningful (if 0 ignore)
        public const int FF_BUFFER_HINTS_READABLE = 0x02; // Codec will read from buffer
        public const int FF_BUFFER_HINTS_PRESERVE = 0x04; // User must not alter buffer content
        public const int FF_BUFFER_HINTS_REUSABLE = 0x08; // Codec will reuse the buffer (update)

        public const int DEFAULT_FRAME_RATE_BASE = 1001000;

        public const int FF_ASPECT_EXTENDED = 15;

        public const int FF_RC_STRATEGY_XVID = 1;

        public const int FF_BUG_AUTODETECT = 1;  // autodetection
        public const int FF_BUG_OLD_MSMPEG4 = 2;
        public const int FF_BUG_XVID_ILACE = 4;
        public const int FF_BUG_UMP4 = 8;
        public const int FF_BUG_NO_PADDING = 16;
        public const int FF_BUG_AMV = 32;
        public const int FF_BUG_AC_VLC = 0;  // will be removed, libavcodec can now handle these non compliant files by default
        public const int FF_BUG_QPEL_CHROMA = 64;
        public const int FF_BUG_STD_QPEL = 128;
        public const int FF_BUG_QPEL_CHROMA2 = 256;
        public const int FF_BUG_DIRECT_BLOCKSIZE = 512;
        public const int FF_BUG_EDGE = 1024;
        public const int FF_BUG_HPEL_CHROMA = 2048;
        public const int FF_BUG_DC_CLIP = 4096;
        public const int FF_BUG_MS = 8192; // workaround various bugs in microsofts broken decoders
        // public const int FF_BUG_FAKE_SCALABILITY =16; //autodetection should work 100%

        public const int FF_COMPLIANCE_VERY_STRICT = 2; // strictly conform to a older more strict version of the spec or reference software
        public const int FF_COMPLIANCE_STRICT = 1; // strictly conform to all the things in the spec no matter what consequences
        public const int FF_COMPLIANCE_NORMAL = 0;
        public const int FF_COMPLIANCE_INOFFICIAL = -1; // allow inofficial extensions
        public const int FF_COMPLIANCE_EXPERIMENTAL = -2; // allow non standarized experimental things

        public const int FF_ER_CAREFUL = 1;
        public const int FF_ER_COMPLIANT = 2;
        public const int FF_ER_AGGRESSIVE = 3;
        public const int FF_ER_VERY_AGGRESSIVE = 4;

        public const int FF_DCT_AUTO = 0;
        public const int FF_DCT_FASTINT = 1;
        public const int FF_DCT_INT = 2;
        public const int FF_DCT_MMX = 3;
        public const int FF_DCT_MLIB = 4;
        public const int FF_DCT_ALTIVEC = 5;
        public const int FF_DCT_FAAN = 6;

        public const int FF_IDCT_AUTO = 0;
        public const int FF_IDCT_INT = 1;
        public const int FF_IDCT_SIMPLE = 2;
        public const int FF_IDCT_SIMPLEMMX = 3;
        public const int FF_IDCT_LIBMPEG2MMX = 4;
        public const int FF_IDCT_PS2 = 5;
        public const int FF_IDCT_MLIB = 6;
        public const int FF_IDCT_ARM = 7;
        public const int FF_IDCT_ALTIVEC = 8;
        public const int FF_IDCT_SH4 = 9;
        public const int FF_IDCT_SIMPLEARM = 10;
        public const int FF_IDCT_H264 = 11;
        public const int FF_IDCT_VP3 = 12;
        public const int FF_IDCT_IPP = 13;
        public const int FF_IDCT_XVIDMMX = 14;
        public const int FF_IDCT_CAVS = 15;

        public const int FF_EC_GUESS_MVS = 1;
        public const int FF_EC_DEBLOCK = 2;

        public const uint FF_MM_FORCE = 0x80000000; /* force usage of selected flags (OR) */
        //    /* lower 16 bits - CPU features */

        public const int FF_MM_MMX = 0x0001; /* standard MMX */
        public const int FF_MM_3DNOW = 0x0004; /* AMD 3DNOW */
        public const int FF_MM_MMXEXT = 0x0002;/* SSE integer functions or AMD MMX ext */
        public const int FF_MM_SSE = 0x0008; /* SSE functions */
        public const int FF_MM_SSE2 = 0x0010;/* PIV SSE2 functions */
        public const int FF_MM_3DNOWEXT = 0x0020;/* AMD 3DNowExt */

        public const int FF_MM_IWMMXT = 0x0100; /* XScale IWMMXT */

        public const int FF_PRED_LEFT = 0;
        public const int FF_PRED_PLANE = 1;
        public const int FF_PRED_MEDIAN = 2;

        public const int FF_DEBUG_PICT_INFO = 1;
        public const int FF_DEBUG_RC = 2;
        public const int FF_DEBUG_BITSTREAM = 4;
        public const int FF_DEBUG_MB_TYPE = 8;
        public const int FF_DEBUG_QP = 16;
        public const int FF_DEBUG_MV = 32;
        public const int FF_DEBUG_DCT_COEFF = 0x00000040;
        public const int FF_DEBUG_SKIP = 0x00000080;
        public const int FF_DEBUG_STARTCODE = 0x00000100;
        public const int FF_DEBUG_PTS = 0x00000200;
        public const int FF_DEBUG_ER = 0x00000400;
        public const int FF_DEBUG_MMCO = 0x00000800;
        public const int FF_DEBUG_BUGS = 0x00001000;
        public const int FF_DEBUG_VIS_QP = 0x00002000;
        public const int FF_DEBUG_VIS_MB_TYPE = 0x00004000;

        public const int FF_DEBUG_VIS_MV_P_FOR = 0x00000001; //visualize forward predicted MVs of P frames
        public const int FF_DEBUG_VIS_MV_B_FOR = 0x00000002; //visualize forward predicted MVs of B frames
        public const int FF_DEBUG_VIS_MV_B_BACK = 0x00000004; //visualize backward predicted MVs of B frames

        public const int FF_CMP_SAD = 0;
        public const int FF_CMP_SSE = 1;
        public const int FF_CMP_SATD = 2;
        public const int FF_CMP_DCT = 3;
        public const int FF_CMP_PSNR = 4;
        public const int FF_CMP_BIT = 5;
        public const int FF_CMP_RD = 6;
        public const int FF_CMP_ZERO = 7;
        public const int FF_CMP_VSAD = 8;
        public const int FF_CMP_VSSE = 9;
        public const int FF_CMP_NSSE = 10;
        public const int FF_CMP_W53 = 11;
        public const int FF_CMP_W97 = 12;
        public const int FF_CMP_DCTMAX = 13;
        public const int FF_CMP_DCT264 = 14;
        public const int FF_CMP_CHROMA = 256;

        public const int FF_DTG_AFD_SAME = 8;
        public const int FF_DTG_AFD_4_3 = 9;
        public const int FF_DTG_AFD_16_9 = 10;
        public const int FF_DTG_AFD_14_9 = 11;
        public const int FF_DTG_AFD_4_3_SP_14_9 = 13;
        public const int FF_DTG_AFD_16_9_SP_14_9 = 14;
        public const int FF_DTG_AFD_SP_4_3 = 15;

        public const int FF_DEFAULT_QUANT_BIAS = 999999;

        public const int FF_LAMBDA_SHIFT = 7;
        public const int FF_LAMBDA_SCALE = (1 << FF_LAMBDA_SHIFT);
        public const int FF_QP2LAMBDA = 118; // factor to convert from H.263 QP to lambda
        public const int FF_LAMBDA_MAX = (256 * 128 - 1);

        public const int FF_CODER_TYPE_VLC = 0;
        public const int FF_CODER_TYPE_AC = 1;

        public const int SLICE_FLAG_CODED_ORDER = 0x0001; // draw_horiz_band() is called in coded order instead of display
        public const int SLICE_FLAG_ALLOW_FIELD = 0x0002; // allow draw_horiz_band() with field slices (MPEG2 field pics)
        public const int SLICE_FLAG_ALLOW_PLANE = 0x0004; // allow draw_horiz_band() with 1 component at a time (SVQ1)


        public const int FF_MB_DECISION_SIMPLE = 0; // uses mb_cmp
        public const int FF_MB_DECISION_BITS = 1; // chooses the one which needs the fewest bits
        public const int FF_MB_DECISION_RD = 2; // rate distoration

        public const int FF_AA_AUTO = 0;
        public const int FF_AA_FASTINT = 1; //not implemented yet
        public const int FF_AA_INT = 2;
        public const int FF_AA_FLOAT = 3;

        public const int FF_PROFILE_UNKNOWN = -99;

        public const int FF_LEVEL_UNKNOWN = -99;

        public const int X264_PART_I4X4 = 0x001; /* Analyse i4x4 */
        public const int X264_PART_I8X8 = 0x002; /* Analyse i8x8 (requires 8x8 transform) */
        public const int X264_PART_P8X8 = 0x010; /* Analyse p16x8, p8x16 and p8x8 */
        public const int X264_PART_P4X4 = 0x020; /* Analyse p8x4, p4x8, p4x4 */
        public const int X264_PART_B8X8 = 0x100; /* Analyse b16x8, b8x16 and b8x8 */

        public const int FF_COMPRESSION_DEFAULT = -1;

        public const int AVPALETTE_SIZE = 1024;
        public const int AVPALETTE_COUNT = 256;

        public const int FF_LOSS_RESOLUTION = 0x0001; /* loss due to resolution change */
        public const int FF_LOSS_DEPTH = 0x0002; /* loss due to color depth change */
        public const int FF_LOSS_COLORSPACE = 0x0004; /* loss due to color space conversion */
        public const int FF_LOSS_ALPHA = 0x0008; /* loss of alpha bits */
        public const int FF_LOSS_COLORQUANT = 0x0010; /* loss due to color quantization */
        public const int FF_LOSS_CHROMA = 0x0020; /* loss of chroma (e.g. rgb to gray conversion) */

        public const int FF_ALPHA_TRANSP = 0x0001; // image has some totally transparent pixels 
        public const int FF_ALPHA_SEMI_TRANSP = 0x0002; // image has some transparent pixels 

        public const int AV_PARSER_PTS_NB = 4;
        public const int AV_NUM_DATA_POINTERS = 8;

        public const int PARSER_FLAG_COMPLETE_FRAMES = 0x0001;


        // *********************************************************************************
        // Enums
        // *********************************************************************************

        /**
         * Identify the syntax and semantics of the bitstream.
         * The principle is roughly:
         * Two decoders with the same ID can decode the same streams.
         * Two encoders with the same ID can encode compatible streams.
         * There may be slight deviations from the principle due to implementation
         * details.
         *
         * If you add a codec ID to this list, add it so that
         * 1. no value of an existing codec ID changes (that would break ABI),
         * 2. it is as close as possible to similar codecs
         *
         * After adding new codec IDs, do not forget to add an entry to the codec
         * descriptor list and bump libavcodec minor version.
         */
        public enum AVCodecID
        {
            AV_CODEC_ID_NONE,

            /* video codecs */
            AV_CODEC_ID_MPEG1VIDEO,
            AV_CODEC_ID_MPEG2VIDEO, //< preferred ID for MPEG-1/2 video decoding
//#if FF_API_XVMC
            AV_CODEC_ID_MPEG2VIDEO_XVMC,
//#endif /* FF_API_XVMC */
            AV_CODEC_ID_H261,
            AV_CODEC_ID_H263,
            AV_CODEC_ID_RV10,
            AV_CODEC_ID_RV20,
            AV_CODEC_ID_MJPEG,
            AV_CODEC_ID_MJPEGB,
            AV_CODEC_ID_LJPEG,
            AV_CODEC_ID_SP5X,
            AV_CODEC_ID_JPEGLS,
            AV_CODEC_ID_MPEG4,
            AV_CODEC_ID_RAWVIDEO,
            AV_CODEC_ID_MSMPEG4V1,
            AV_CODEC_ID_MSMPEG4V2,
            AV_CODEC_ID_MSMPEG4V3,
            AV_CODEC_ID_WMV1,
            AV_CODEC_ID_WMV2,
            AV_CODEC_ID_H263P,
            AV_CODEC_ID_H263I,
            AV_CODEC_ID_FLV1,
            AV_CODEC_ID_SVQ1,
            AV_CODEC_ID_SVQ3,
            AV_CODEC_ID_DVVIDEO,
            AV_CODEC_ID_HUFFYUV,
            AV_CODEC_ID_CYUV,
            AV_CODEC_ID_H264,
            AV_CODEC_ID_INDEO3,
            AV_CODEC_ID_VP3,
            AV_CODEC_ID_THEORA,
            AV_CODEC_ID_ASV1,
            AV_CODEC_ID_ASV2,
            AV_CODEC_ID_FFV1,
            AV_CODEC_ID_4XM,
            AV_CODEC_ID_VCR1,
            AV_CODEC_ID_CLJR,
            AV_CODEC_ID_MDEC,
            AV_CODEC_ID_ROQ,
            AV_CODEC_ID_INTERPLAY_VIDEO,
            AV_CODEC_ID_XAN_WC3,
            AV_CODEC_ID_XAN_WC4,
            AV_CODEC_ID_RPZA,
            AV_CODEC_ID_CINEPAK,
            AV_CODEC_ID_WS_VQA,
            AV_CODEC_ID_MSRLE,
            AV_CODEC_ID_MSVIDEO1,
            AV_CODEC_ID_IDCIN,
            AV_CODEC_ID_8BPS,
            AV_CODEC_ID_SMC,
            AV_CODEC_ID_FLIC,
            AV_CODEC_ID_TRUEMOTION1,
            AV_CODEC_ID_VMDVIDEO,
            AV_CODEC_ID_MSZH,
            AV_CODEC_ID_ZLIB,
            AV_CODEC_ID_QTRLE,
            AV_CODEC_ID_TSCC,
            AV_CODEC_ID_ULTI,
            AV_CODEC_ID_QDRAW,
            AV_CODEC_ID_VIXL,
            AV_CODEC_ID_QPEG,
            AV_CODEC_ID_PNG,
            AV_CODEC_ID_PPM,
            AV_CODEC_ID_PBM,
            AV_CODEC_ID_PGM,
            AV_CODEC_ID_PGMYUV,
            AV_CODEC_ID_PAM,
            AV_CODEC_ID_FFVHUFF,
            AV_CODEC_ID_RV30,
            AV_CODEC_ID_RV40,
            AV_CODEC_ID_VC1,
            AV_CODEC_ID_WMV3,
            AV_CODEC_ID_LOCO,
            AV_CODEC_ID_WNV1,
            AV_CODEC_ID_AASC,
            AV_CODEC_ID_INDEO2,
            AV_CODEC_ID_FRAPS,
            AV_CODEC_ID_TRUEMOTION2,
            AV_CODEC_ID_BMP,
            AV_CODEC_ID_CSCD,
            AV_CODEC_ID_MMVIDEO,
            AV_CODEC_ID_ZMBV,
            AV_CODEC_ID_AVS,
            AV_CODEC_ID_SMACKVIDEO,
            AV_CODEC_ID_NUV,
            AV_CODEC_ID_KMVC,
            AV_CODEC_ID_FLASHSV,
            AV_CODEC_ID_CAVS,
            AV_CODEC_ID_JPEG2000,
            AV_CODEC_ID_VMNC,
            AV_CODEC_ID_VP5,
            AV_CODEC_ID_VP6,
            AV_CODEC_ID_VP6F,
            AV_CODEC_ID_TARGA,
            AV_CODEC_ID_DSICINVIDEO,
            AV_CODEC_ID_TIERTEXSEQVIDEO,
            AV_CODEC_ID_TIFF,
            AV_CODEC_ID_GIF,
            AV_CODEC_ID_DXA,
            AV_CODEC_ID_DNXHD,
            AV_CODEC_ID_THP,
            AV_CODEC_ID_SGI,
            AV_CODEC_ID_C93,
            AV_CODEC_ID_BETHSOFTVID,
            AV_CODEC_ID_PTX,
            AV_CODEC_ID_TXD,
            AV_CODEC_ID_VP6A,
            AV_CODEC_ID_AMV,
            AV_CODEC_ID_VB,
            AV_CODEC_ID_PCX,
            AV_CODEC_ID_SUNRAST,
            AV_CODEC_ID_INDEO4,
            AV_CODEC_ID_INDEO5,
            AV_CODEC_ID_MIMIC,
            AV_CODEC_ID_RL2,
            AV_CODEC_ID_ESCAPE124,
            AV_CODEC_ID_DIRAC,
            AV_CODEC_ID_BFI,
            AV_CODEC_ID_CMV,
            AV_CODEC_ID_MOTIONPIXELS,
            AV_CODEC_ID_TGV,
            AV_CODEC_ID_TGQ,
            AV_CODEC_ID_TQI,
            AV_CODEC_ID_AURA,
            AV_CODEC_ID_AURA2,
            AV_CODEC_ID_V210X,
            AV_CODEC_ID_TMV,
            AV_CODEC_ID_V210,
            AV_CODEC_ID_DPX,
            AV_CODEC_ID_MAD,
            AV_CODEC_ID_FRWU,
            AV_CODEC_ID_FLASHSV2,
            AV_CODEC_ID_CDGRAPHICS,
            AV_CODEC_ID_R210,
            AV_CODEC_ID_ANM,
            AV_CODEC_ID_BINKVIDEO,
            AV_CODEC_ID_IFF_ILBM,
//#define AV_CODEC_ID_IFF_BYTERUN1 AV_CODEC_ID_IFF_ILBM
            AV_CODEC_ID_KGV1,
            AV_CODEC_ID_YOP,
            AV_CODEC_ID_VP8,
            AV_CODEC_ID_PICTOR,
            AV_CODEC_ID_ANSI,
            AV_CODEC_ID_A64_MULTI,
            AV_CODEC_ID_A64_MULTI5,
            AV_CODEC_ID_R10K,
            AV_CODEC_ID_MXPEG,
            AV_CODEC_ID_LAGARITH,
            AV_CODEC_ID_PRORES,
            AV_CODEC_ID_JV,
            AV_CODEC_ID_DFA,
            AV_CODEC_ID_WMV3IMAGE,
            AV_CODEC_ID_VC1IMAGE,
            AV_CODEC_ID_UTVIDEO,
            AV_CODEC_ID_BMV_VIDEO,
            AV_CODEC_ID_VBLE,
            AV_CODEC_ID_DXTORY,
            AV_CODEC_ID_V410,
            AV_CODEC_ID_XWD,
            AV_CODEC_ID_CDXL,
            AV_CODEC_ID_XBM,
            AV_CODEC_ID_ZEROCODEC,
            AV_CODEC_ID_MSS1,
            AV_CODEC_ID_MSA1,
            AV_CODEC_ID_TSCC2,
            AV_CODEC_ID_MTS2,
            AV_CODEC_ID_CLLC,
            AV_CODEC_ID_MSS2,
            AV_CODEC_ID_VP9,
            AV_CODEC_ID_AIC,
            AV_CODEC_ID_ESCAPE130,
            AV_CODEC_ID_G2M,
            AV_CODEC_ID_WEBP,
            AV_CODEC_ID_HNM4_VIDEO,
            AV_CODEC_ID_HEVC,
//#define AV_CODEC_ID_H265 AV_CODEC_ID_HEVC
            AV_CODEC_ID_FIC,
            AV_CODEC_ID_ALIAS_PIX,
            AV_CODEC_ID_BRENDER_PIX,
            AV_CODEC_ID_PAF_VIDEO,
            AV_CODEC_ID_EXR,
            AV_CODEC_ID_VP7,
            AV_CODEC_ID_SANM,
            AV_CODEC_ID_SGIRLE,
            AV_CODEC_ID_MVC1,
            AV_CODEC_ID_MVC2,
            AV_CODEC_ID_HQX,
            AV_CODEC_ID_TDSC,
            AV_CODEC_ID_HQ_HQA,
            AV_CODEC_ID_HAP,
            AV_CODEC_ID_DDS,
            AV_CODEC_ID_DXV,
            AV_CODEC_ID_SCREENPRESSO,
            AV_CODEC_ID_RSCC,

            AV_CODEC_ID_Y41P = 0x8000,
            AV_CODEC_ID_AVRP,
            AV_CODEC_ID_012V,
            AV_CODEC_ID_AVUI,
            AV_CODEC_ID_AYUV,
            AV_CODEC_ID_TARGA_Y216,
            AV_CODEC_ID_V308,
            AV_CODEC_ID_V408,
            AV_CODEC_ID_YUV4,
            AV_CODEC_ID_AVRN,
            AV_CODEC_ID_CPIA,
            AV_CODEC_ID_XFACE,
            AV_CODEC_ID_SNOW,
            AV_CODEC_ID_SMVJPEG,
            AV_CODEC_ID_APNG,
            AV_CODEC_ID_DAALA,
            AV_CODEC_ID_CFHD,
            AV_CODEC_ID_TRUEMOTION2RT,
            AV_CODEC_ID_M101,
            AV_CODEC_ID_MAGICYUV,
            AV_CODEC_ID_SHEERVIDEO,
            AV_CODEC_ID_YLC,
            AV_CODEC_ID_PSD,
            AV_CODEC_ID_PIXLET,
            AV_CODEC_ID_SPEEDHQ,
            AV_CODEC_ID_FMVC,
            AV_CODEC_ID_SCPR,
            AV_CODEC_ID_CLEARVIDEO,
            AV_CODEC_ID_XPM,
            AV_CODEC_ID_AV1,
            AV_CODEC_ID_BITPACKED,
            AV_CODEC_ID_MSCC,
            AV_CODEC_ID_SRGC,
            AV_CODEC_ID_SVG,
            AV_CODEC_ID_GDV,

            /* various PCM "codecs" */
            AV_CODEC_ID_FIRST_AUDIO = 0x10000,     //< A dummy id pointing at the start of audio codecs
            AV_CODEC_ID_PCM_S16LE = 0x10000,
            AV_CODEC_ID_PCM_S16BE,
            AV_CODEC_ID_PCM_U16LE,
            AV_CODEC_ID_PCM_U16BE,
            AV_CODEC_ID_PCM_S8,
            AV_CODEC_ID_PCM_U8,
            AV_CODEC_ID_PCM_MULAW,
            AV_CODEC_ID_PCM_ALAW,
            AV_CODEC_ID_PCM_S32LE,
            AV_CODEC_ID_PCM_S32BE,
            AV_CODEC_ID_PCM_U32LE,
            AV_CODEC_ID_PCM_U32BE,
            AV_CODEC_ID_PCM_S24LE,
            AV_CODEC_ID_PCM_S24BE,
            AV_CODEC_ID_PCM_U24LE,
            AV_CODEC_ID_PCM_U24BE,
            AV_CODEC_ID_PCM_S24DAUD,
            AV_CODEC_ID_PCM_ZORK,
            AV_CODEC_ID_PCM_S16LE_PLANAR,
            AV_CODEC_ID_PCM_DVD,
            AV_CODEC_ID_PCM_F32BE,
            AV_CODEC_ID_PCM_F32LE,
            AV_CODEC_ID_PCM_F64BE,
            AV_CODEC_ID_PCM_F64LE,
            AV_CODEC_ID_PCM_BLURAY,
            AV_CODEC_ID_PCM_LXF,
            AV_CODEC_ID_S302M,
            AV_CODEC_ID_PCM_S8_PLANAR,
            AV_CODEC_ID_PCM_S24LE_PLANAR,
            AV_CODEC_ID_PCM_S32LE_PLANAR,
            AV_CODEC_ID_PCM_S16BE_PLANAR,

            AV_CODEC_ID_PCM_S64LE = 0x10800,
            AV_CODEC_ID_PCM_S64BE,
            AV_CODEC_ID_PCM_F16LE,
            AV_CODEC_ID_PCM_F24LE,

            /* various ADPCM codecs */
            AV_CODEC_ID_ADPCM_IMA_QT = 0x11000,
            AV_CODEC_ID_ADPCM_IMA_WAV,
            AV_CODEC_ID_ADPCM_IMA_DK3,
            AV_CODEC_ID_ADPCM_IMA_DK4,
            AV_CODEC_ID_ADPCM_IMA_WS,
            AV_CODEC_ID_ADPCM_IMA_SMJPEG,
            AV_CODEC_ID_ADPCM_MS,
            AV_CODEC_ID_ADPCM_4XM,
            AV_CODEC_ID_ADPCM_XA,
            AV_CODEC_ID_ADPCM_ADX,
            AV_CODEC_ID_ADPCM_EA,
            AV_CODEC_ID_ADPCM_G726,
            AV_CODEC_ID_ADPCM_CT,
            AV_CODEC_ID_ADPCM_SWF,
            AV_CODEC_ID_ADPCM_YAMAHA,
            AV_CODEC_ID_ADPCM_SBPRO_4,
            AV_CODEC_ID_ADPCM_SBPRO_3,
            AV_CODEC_ID_ADPCM_SBPRO_2,
            AV_CODEC_ID_ADPCM_THP,
            AV_CODEC_ID_ADPCM_IMA_AMV,
            AV_CODEC_ID_ADPCM_EA_R1,
            AV_CODEC_ID_ADPCM_EA_R3,
            AV_CODEC_ID_ADPCM_EA_R2,
            AV_CODEC_ID_ADPCM_IMA_EA_SEAD,
            AV_CODEC_ID_ADPCM_IMA_EA_EACS,
            AV_CODEC_ID_ADPCM_EA_XAS,
            AV_CODEC_ID_ADPCM_EA_MAXIS_XA,
            AV_CODEC_ID_ADPCM_IMA_ISS,
            AV_CODEC_ID_ADPCM_G722,
            AV_CODEC_ID_ADPCM_IMA_APC,
            AV_CODEC_ID_ADPCM_VIMA,
//#if FF_API_VIMA_DECODER
//    AV_CODEC_ID_VIMA = AV_CODEC_ID_ADPCM_VIMA,
//#endif

            AV_CODEC_ID_ADPCM_AFC = 0x11800,
            AV_CODEC_ID_ADPCM_IMA_OKI,
            AV_CODEC_ID_ADPCM_DTK,
            AV_CODEC_ID_ADPCM_IMA_RAD,
            AV_CODEC_ID_ADPCM_G726LE,
            AV_CODEC_ID_ADPCM_THP_LE,
            AV_CODEC_ID_ADPCM_PSX,
            AV_CODEC_ID_ADPCM_AICA,
            AV_CODEC_ID_ADPCM_IMA_DAT4,
            AV_CODEC_ID_ADPCM_MTAF,

            /* AMR */
            AV_CODEC_ID_AMR_NB = 0x12000,
            AV_CODEC_ID_AMR_WB,

            /* RealAudio codecs*/
            AV_CODEC_ID_RA_144 = 0x13000,
            AV_CODEC_ID_RA_288,

            /* various DPCM codecs */
            AV_CODEC_ID_ROQ_DPCM = 0x14000,
            AV_CODEC_ID_INTERPLAY_DPCM,
            AV_CODEC_ID_XAN_DPCM,
            AV_CODEC_ID_SOL_DPCM,

            AV_CODEC_ID_SDX2_DPCM = 0x14800,
            AV_CODEC_ID_GREMLIN_DPCM,

            /* audio codecs */
            AV_CODEC_ID_MP2 = 0x15000,
            AV_CODEC_ID_MP3, //< preferred ID for decoding MPEG audio layer 1, 2 or 3
            AV_CODEC_ID_AAC,
            AV_CODEC_ID_AC3,
            AV_CODEC_ID_DTS,
            AV_CODEC_ID_VORBIS,
            AV_CODEC_ID_DVAUDIO,
            AV_CODEC_ID_WMAV1,
            AV_CODEC_ID_WMAV2,
            AV_CODEC_ID_MACE3,
            AV_CODEC_ID_MACE6,
            AV_CODEC_ID_VMDAUDIO,
            AV_CODEC_ID_FLAC,
            AV_CODEC_ID_MP3ADU,
            AV_CODEC_ID_MP3ON4,
            AV_CODEC_ID_SHORTEN,
            AV_CODEC_ID_ALAC,
            AV_CODEC_ID_WESTWOOD_SND1,
            AV_CODEC_ID_GSM, //< as in Berlin toast format
            AV_CODEC_ID_QDM2,
            AV_CODEC_ID_COOK,
            AV_CODEC_ID_TRUESPEECH,
            AV_CODEC_ID_TTA,
            AV_CODEC_ID_SMACKAUDIO,
            AV_CODEC_ID_QCELP,
            AV_CODEC_ID_WAVPACK,
            AV_CODEC_ID_DSICINAUDIO,
            AV_CODEC_ID_IMC,
            AV_CODEC_ID_MUSEPACK7,
            AV_CODEC_ID_MLP,
            AV_CODEC_ID_GSM_MS, /* as found in WAV */
            AV_CODEC_ID_ATRAC3,
//#if FF_API_VOXWARE
            AV_CODEC_ID_VOXWARE,
//#endif
            AV_CODEC_ID_APE,
            AV_CODEC_ID_NELLYMOSER,
            AV_CODEC_ID_MUSEPACK8,
            AV_CODEC_ID_SPEEX,
            AV_CODEC_ID_WMAVOICE,
            AV_CODEC_ID_WMAPRO,
            AV_CODEC_ID_WMALOSSLESS,
            AV_CODEC_ID_ATRAC3P,
            AV_CODEC_ID_EAC3,
            AV_CODEC_ID_SIPR,
            AV_CODEC_ID_MP1,
            AV_CODEC_ID_TWINVQ,
            AV_CODEC_ID_TRUEHD,
            AV_CODEC_ID_MP4ALS,
            AV_CODEC_ID_ATRAC1,
            AV_CODEC_ID_BINKAUDIO_RDFT,
            AV_CODEC_ID_BINKAUDIO_DCT,
            AV_CODEC_ID_AAC_LATM,
            AV_CODEC_ID_QDMC,
            AV_CODEC_ID_CELT,
            AV_CODEC_ID_G723_1,
            AV_CODEC_ID_G729,
            AV_CODEC_ID_8SVX_EXP,
            AV_CODEC_ID_8SVX_FIB,
            AV_CODEC_ID_BMV_AUDIO,
            AV_CODEC_ID_RALF,
            AV_CODEC_ID_IAC,
            AV_CODEC_ID_ILBC,
            AV_CODEC_ID_OPUS,
            AV_CODEC_ID_COMFORT_NOISE,
            AV_CODEC_ID_TAK,
            AV_CODEC_ID_METASOUND,
            AV_CODEC_ID_PAF_AUDIO,
            AV_CODEC_ID_ON2AVC,
            AV_CODEC_ID_DSS_SP,

            AV_CODEC_ID_FFWAVESYNTH = 0x15800,
            AV_CODEC_ID_SONIC,
            AV_CODEC_ID_SONIC_LS,
            AV_CODEC_ID_EVRC,
            AV_CODEC_ID_SMV,
            AV_CODEC_ID_DSD_LSBF,
            AV_CODEC_ID_DSD_MSBF,
            AV_CODEC_ID_DSD_LSBF_PLANAR,
            AV_CODEC_ID_DSD_MSBF_PLANAR,
            AV_CODEC_ID_4GV,
            AV_CODEC_ID_INTERPLAY_ACM,
            AV_CODEC_ID_XMA1,
            AV_CODEC_ID_XMA2,
            AV_CODEC_ID_DST,
            AV_CODEC_ID_ATRAC3AL,
            AV_CODEC_ID_ATRAC3PAL,
            AV_CODEC_ID_DOLBY_E,

            /* subtitle codecs */
            AV_CODEC_ID_FIRST_SUBTITLE = 0x17000,   //< A dummy ID pointing at the start of subtitle codecs.
            AV_CODEC_ID_DVD_SUBTITLE = 0x17000,
            AV_CODEC_ID_DVB_SUBTITLE,
            AV_CODEC_ID_TEXT,                       //< raw UTF-8 text
            AV_CODEC_ID_XSUB,
            AV_CODEC_ID_SSA,
            AV_CODEC_ID_MOV_TEXT,
            AV_CODEC_ID_HDMV_PGS_SUBTITLE,
            AV_CODEC_ID_DVB_TELETEXT,
            AV_CODEC_ID_SRT,

            AV_CODEC_ID_MICRODVD = 0x17800,
            AV_CODEC_ID_EIA_608,
            AV_CODEC_ID_JACOSUB,
            AV_CODEC_ID_SAMI,
            AV_CODEC_ID_REALTEXT,
            AV_CODEC_ID_STL,
            AV_CODEC_ID_SUBVIEWER1,
            AV_CODEC_ID_SUBVIEWER,
            AV_CODEC_ID_SUBRIP,
            AV_CODEC_ID_WEBVTT,
            AV_CODEC_ID_MPL2,
            AV_CODEC_ID_VPLAYER,
            AV_CODEC_ID_PJS,
            AV_CODEC_ID_ASS,
            AV_CODEC_ID_HDMV_TEXT_SUBTITLE,

            /* other specific kind of codecs (generally used for attachments) */
            AV_CODEC_ID_FIRST_UNKNOWN = 0x18000,           //< A dummy ID pointing at the start of various fake codecs.
            AV_CODEC_ID_TTF = 0x18000,

            AV_CODEC_ID_SCTE_35,                            //< Contain timestamp estimated through PCR of program stream.
            AV_CODEC_ID_BINTEXT = 0x18800,
            AV_CODEC_ID_XBIN,
            AV_CODEC_ID_IDF,
            AV_CODEC_ID_OTF,
            AV_CODEC_ID_SMPTE_KLV,
            AV_CODEC_ID_DVD_NAV,
            AV_CODEC_ID_TIMED_ID3,
            AV_CODEC_ID_BIN_DATA,


            AV_CODEC_ID_PROBE = 0x19000,            //< codec_id is not known (like AV_CODEC_ID_NONE) but lavf should attempt to identify it

            AV_CODEC_ID_MPEG2TS = 0x20000,          // _FAKE_ codec to indicate a raw MPEG-2 TS
                                                    // stream (only used by libavformat)
            AV_CODEC_ID_MPEG4SYSTEMS = 0x20001,     //*< _FAKE_ codec to indicate a MPEG-4 Systems
                                                    //* stream (only used by libavformat) */
            AV_CODEC_ID_FFMETADATA = 0x21000,       //< Dummy codec for streams containing only metadata information.
            AV_CODEC_ID_WRAPPED_AVFRAME = 0x21001,  //< Passthrough codec, AVFrames wrapped in AVPacket
        };

        /* currently unused, may be used if 24/32 bits samples ever supported */
        /* all in native endian */
        public enum SampleFormat
        {
            SAMPLE_FMT_NONE = -1,
            SAMPLE_FMT_U8,              ///< unsigned 8 bits
            SAMPLE_FMT_S16,             ///< signed 16 bits
            SAMPLE_FMT_S24,             ///< signed 24 bits
            SAMPLE_FMT_S32,             ///< signed 32 bits
            SAMPLE_FMT_FLT,             ///< float
        };

        public enum Motion_Est_ID
        {
            ME_ZERO = 1,
            ME_FULL,
            ME_LOG,
            ME_PHODS,
            ME_EPZS,
            ME_X1,
            ME_HEX,
            ME_UMH,
            ME_ITER,
        };

        public enum AVDiscard
        {
            //we leave some space between them for extensions (drop some keyframes for intra only or drop just some bidir frames)
            AVDISCARD_NONE = -16, ///< discard nothing
            AVDISCARD_DEFAULT = 0, ///< discard useless packets like 0 size packets in avi
            AVDISCARD_NONREF = 8, ///< discard all non reference
            AVDISCARD_BIDIR = 16, ///< discard all bidirectional frames
            AVDISCARD_NONKEY = 32, ///< discard all frames except keyframes
            AVDISCARD_ALL = 48, ///< discard all
        };

        // *********************************************************************************
        // Structs
        // *********************************************************************************

        [StructLayout(LayoutKind.Sequential)]
        public struct RcOverride
        {
            [MarshalAs(UnmanagedType.I4)]
            public int start_frame;

            [MarshalAs(UnmanagedType.I4)]
            public int end_frame;

            [MarshalAs(UnmanagedType.I4)]
            public int qscale;

            [MarshalAs(UnmanagedType.R4)]
            public float quality_factor;
        };

        public struct AVPanScan
        {
            /**
             * id.
             * - encoding: set by user.
             * - decoding: set by lavc
             */
            public int id;
            /**
             * width and height in 1/16 pel
             * - encoding: set by user.
             * - decoding: set by lavc
             */
            public int width;
            public int height;
            /**
             * position of the top left corner in 1/16 pel for up to 3 fields/frames.
             * - encoding: set by user.
             * - decoding: set by lavc
             */
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            // [3][2] = 3 x 2 = 6
            public short[] position;
        };

        public delegate void OpaqueCallback();


        /**
        * This structure describes decoded (raw) audio or video data.
        *
        * AVFrame must be allocated using av_frame_alloc(). Note that this only
        * allocates the AVFrame itself, the buffers for the data must be managed
        * through other means (see below).
        * AVFrame must be freed with av_frame_free().
        *
        * AVFrame is typically allocated once and then reused multiple times to hold
        * different data (e.g. a single AVFrame to hold frames received from a
        * decoder). In such a case, av_frame_unref() will free any references held by
        * the frame and reset it to its original clean state before it
        * is reused again.
        *
        * The data described by an AVFrame is usually reference counted through the
        * AVBuffer API. The underlying buffer references are stored in AVFrame.buf /
        * AVFrame.extended_buf. An AVFrame is considered to be reference counted if at
        * least one reference is set, i.e. if AVFrame.buf[0] != NULL. In such a case,
        * every single data plane must be contained in one of the buffers in
        * AVFrame.buf or AVFrame.extended_buf.
        * There may be a single buffer for all the data, or one separate buffer for
        * each plane, or anything in between.
        *
        * sizeof(AVFrame) is not a part of the public ABI, so new fields may be added
        * to the end with a minor bump.
        *
        * Fields can be accessed through AVOptions, the name string used, matches the
        * C structure field name for fields accessible through AVOptions. The AVClass
        * for AVFrame can be obtained from avcodec_get_frame_class()
        */
        public class AVFrame
        {
            /**
            * pointer to the picture/channel planes.
            * This might be different from the first allocated byte
            *
            * Some decoders access areas outside 0,0 - width,height, please
            * see avcodec_align_dimensions2(). Some filters and swscale can read
            * up to 16 bytes beyond the planes, if these filters are to be used,
            * then 16 extra bytes must be allocated.
            *
            * NOTE: Except for hwaccel formats, pointers not needed by the format
            * MUST be set to NULL.
            */
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = AV_NUM_DATA_POINTERS)]
            public byte[] data;

            /**
            * For video, size in bytes of each picture line.
            * For audio, size in bytes of each plane.
            *
            * For audio, only linesize[0] may be set. For planar audio, each channel
            * plane must be the same size.
            *
            * For video the linesizes should be multiples of the CPUs alignment
            * preference, this is 16 or 32 for modern desktop CPUs.
            * Some code requires such alignment other code can be slower without
            * correct alignment, for yet other it makes no difference.
            *
            * @note The linesize may be larger than the size of usable data -- there
            * may be extra padding present for performance reasons
            */
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = AV_NUM_DATA_POINTERS)]
            public int[] linesize;

            /**
            * pointers to the data planes/channels.
            *
            * For video, this should simply point to data[].
            *
            * For planar audio, each channel has a separate data pointer, and
            * linesize[0] contains the size of each channel buffer.
            * For packed audio, there is just one data pointer, and linesize[0]
            * contains the total size of the buffer for all channels.
            *
            * Note: Both data and extended_data should always be set in a valid frame,
            * but for planar audio with more channels that can fit in data,
            * extended_data must be used in order to access all channels.
            */
            public IntPtr extended_data;

            /**
            * @name Video dimensions
            * Video frames only. The coded dimensions (in pixels) of the video frame,
            * i.e. the size of the rectangle that contains some well-defined values.
            *
            * @note The part of the frame intended for display/presentation is further
            * restricted by the @ref cropping "Cropping rectangle".
            * @{
            */
            [MarshalAs(UnmanagedType.I4)]
            public int width;

            [MarshalAs(UnmanagedType.I4)]
            public int height;

            /**
            * number of audio samples (per channel) described by this frame
            */
            [MarshalAs(UnmanagedType.I4)]
            public int nb_samples;

            /**
            * format of the frame, -1 if unknown or unset
            * Values correspond to enum AVPixelFormat for video frames,
            * enum AVSampleFormat for audio)
            */
            [MarshalAs(UnmanagedType.I4)]
            public int format;

            /**
             * 1 -> keyframe, 0-> not
             */
            [MarshalAs(UnmanagedType.I4)]
            public int key_frame;

            /**
             * Picture type of the frame.
             */
            public AVPictureType pict_type;

            /**
             * Sample aspect ratio for the video frame, 0/1 if unknown/unspecified.
             */
            public AVRational sample_aspect_ratio;

            /**
             * Presentation timestamp in time_base units (time when frame should be shown to user).
             */
            [MarshalAs(UnmanagedType.I8)]
            public Int64 pts;

            /**
             * DTS copied from the AVPacket that triggered returning this frame. (if frame threading isn't used)
             * This is also the Presentation time of this AVFrame calculated from
             * only AVPacket.dts values without pts values.
             */
            [MarshalAs(UnmanagedType.I8)]
            public Int64 pkt_dts;

            /**
             * picture number in bitstream order
             */
            [MarshalAs(UnmanagedType.I4)]
            public int coded_picture_number;
            /**
             * picture number in display order
             */
            [MarshalAs(UnmanagedType.I4)]
            public int display_picture_number;

            /**
             * quality (between 1 (good) and FF_LAMBDA_MAX (bad))
             */
            [MarshalAs(UnmanagedType.I4)]
            public int quality;

            /**
             * for some private data of the user
             */
             [MarshalAs(UnmanagedType.FunctionPtr)]
            public OpaqueCallback opaque;

            /**
             * When decoding, this signals how much the picture must be delayed.
             * extra_delay = repeat_pict / (2*fps)
             */
            [MarshalAs(UnmanagedType.I4)]
            public int repeat_pict;

            /**
             * The content of the picture is interlaced.
             */
            [MarshalAs(UnmanagedType.I4)]
            public int interlaced_frame;

            /**
             * If the content is interlaced, is top field displayed first.
             */
            [MarshalAs(UnmanagedType.I4)]
            public int top_field_first;

            /**
             * Tell user application that palette has changed from previous frame.
             */
            [MarshalAs(UnmanagedType.I4)]
            public int palette_has_changed;

            /**
             * reordered opaque 64 bits (generally an integer or a double precision float
             * PTS but can be anything).
             * The user sets AVCodecContext.reordered_opaque to represent the input at
             * that time,
             * the decoder reorders values as needed and sets AVFrame.reordered_opaque
             * to exactly one of the values provided by the user through AVCodecContext.reordered_opaque
             * @deprecated in favor of pkt_pts
             */
            [MarshalAs(UnmanagedType.I8)]
            public Int64 reordered_opaque;

            /**
             * Sample rate of the audio data.
             */
            [MarshalAs(UnmanagedType.I4)]
            public int sample_rate;

            /**
             * Channel layout of the audio data.
             */
            [MarshalAs(UnmanagedType.U8)]
            public UInt64 channel_layout;

            //*
            // * AVBuffer references backing the data for this frame.If all elements of
            // * this array are NULL, then this frame is not reference counted.This array
            // * must be filled contiguously -- if buf[i] is non-NULL then buf[j] must
            // * also be non-NULL for all j < i.
            // *
            // * There may be at most one AVBuffer per data plane, so for video this array
            // * always contains all the references. For planar audio with more than
            // * AV_NUM_DATA_POINTERS channels, there may be more buffers than can fit in
            // * this array.Then the extra AVBufferRef pointers are stored in the
            // * extended_buf array.
            public IntPtr buf;

            /**
             * For planar audio which requires more than AV_NUM_DATA_POINTERS
             * AVBufferRef pointers, this array will hold all the references which
             * cannot fit into AVFrame.buf.
             *
             * Note that this is different from AVFrame.extended_data, which always
             * contains all the pointers. This array only contains the extra pointers,
             * which cannot fit into AVFrame.buf.
             *
             * This array is always allocated using av_malloc() by whoever constructs
             * the frame. It is freed in av_frame_unref().
             */
            public IntPtr extended_buf;
            /**
             * Number of elements in extended_buf.
             */
            [MarshalAs(UnmanagedType.I4)]
            public int nb_extended_buf;

            public IntPtr side_data;

            [MarshalAs(UnmanagedType.I4)]
            public int nb_side_data;

            /**
             * Frame flags, a combination of @ref lavu_frame_flags
             */
            [MarshalAs(UnmanagedType.I4)]
            public int flags;

            /**
             * MPEG vs JPEG YUV range.
             * - encoding: Set by user
             * - decoding: Set by libavcodec
             */
            public AVColorRange color_range;

            public AVColorPrimaries color_primaries;

            public AVColorTransferCharacteristic color_trc;

            /**
             * YUV colorspace type.
             * - encoding: Set by user
             * - decoding: Set by libavcodec
             */
            public AVColorSpace colorspace;

            public AVChromaLocation chroma_location;

            /**
             * frame timestamp estimated using various heuristics, in stream time base
             * - encoding: unused
             * - decoding: set by libavcodec, read by user.
             */
            [MarshalAs(UnmanagedType.I8)]
            public Int64 best_effort_timestamp;

            /**
             * reordered pos from the last AVPacket that has been input into the decoder
             * - encoding: unused
             * - decoding: Read by user.
             */
            [MarshalAs(UnmanagedType.I8)]
            public Int64 pkt_pos;

            /**
             * duration of the corresponding packet, expressed in
             * AVStream->time_base units, 0 if unknown.
             * - encoding: unused
             * - decoding: Read by user.
             */
            [MarshalAs(UnmanagedType.I8)]
            public Int64 pkt_duration;

            /**
             * metadata.
             * - encoding: Set by user.
             * - decoding: Set by libavcodec.
             */
            public IntPtr metadata;

            /**
             * decode error flags of the frame, set to a combination of
             * FF_DECODE_ERROR_xxx flags if the decoder produced a frame, but there
             * were errors during the decoding.
             * - encoding: unused
             * - decoding: set by libavcodec, read by user.
             */
            [MarshalAs(UnmanagedType.I4)]
            public int decode_error_flags;

            /**
             * number of audio channels, only used for audio.
             * - encoding: unused
             * - decoding: Read by user.
             */
            [MarshalAs(UnmanagedType.I4)]
            public int channels;

            /**
             * size of the corresponding packet containing the compressed
             * frame.
             * It is set to a negative value if unknown.
             * - encoding: unused
             * - decoding: set by libavcodec, read by user.
             */
            [MarshalAs(UnmanagedType.I4)]
            public int pkt_size;

            /**
             * For hwaccel-format frames, this should be a reference to the
             * AVHWFramesContext describing the frame.
             */
            public IntPtr hw_frames_ctx;

            /**
             * AVBufferRef for free use by the API user. FFmpeg will never check the
             * contents of the buffer ref. FFmpeg calls av_buffer_unref() on it when
             * the frame is unreferenced. av_frame_copy_props() calls create a new
             * reference with av_buffer_ref() for the target frame's opaque_ref field.
             *
             * This is unrelated to the opaque field, although it serves a similar
             * purpose.
             */
            public IntPtr opaque_ref;

            /**
             * @anchor cropping
             * @name Cropping
             * Video frames only. The number of pixels to discard from the the
             * top/bottom/left/right border of the frame to obtain the sub-rectangle of
             * the frame intended for presentation.
             * @{
             */
            [MarshalAs(UnmanagedType.U4)]
            public UInt32 crop_top;
            [MarshalAs(UnmanagedType.U4)]
            public UInt32 crop_bottom;
            [MarshalAs(UnmanagedType.U4)]
            public UInt32 crop_left;
            [MarshalAs(UnmanagedType.U4)]
            public UInt32 crop_right;

        };

        public delegate void DrawhorizBandCallback(AVCodecContext pAVCodecContext, AVFrame pAVFrame,
                                            [MarshalAs(UnmanagedType.LPArray, SizeConst = 4)]int[] offset,
                                            int y, int type, int height);

        public delegate void RtpCallback(AVCodecContext pAVCodecContext, IntPtr pdata, int size, int mb_nb);

        public delegate int GetBufferCallback(AVCodecContext pAVCodecContext, AVFrame pAVFrame, int flags);

        public delegate void ReleaseBufferCallback(AVCodecContext pAVCodecContext, AVFrame pAVFrame);

        public delegate AVPixelFormat GetFormatCallback(AVCodecContext pAVCodecContext, IntPtr pPixelFormat);

        public delegate int RegetBufferCallback(AVCodecContext pAVCodecContext, AVFrame pAVFrame);

        public delegate int FuncCallback(AVCodecContext pAVCodecContext, IntPtr parg);

        public delegate int ExecuteCallback(AVCodecContext pAVCodecContext,
                                            [MarshalAs(UnmanagedType.FunctionPtr)]FuncCallback func,
                                            [MarshalAs(UnmanagedType.LPArray)]IntPtr[] arg2, ref int ret, int count, int size);

        public delegate int Execute2Callback(AVCodecContext pAVCodecContext,
                                    [MarshalAs(UnmanagedType.FunctionPtr)]FuncCallback func,
                                    [MarshalAs(UnmanagedType.LPArray)]IntPtr[] arg2, ref int ret, int count);

        public delegate void GetHWAccelContext();

        [StructLayout(LayoutKind.Sequential)]
        public class AVCodecContext
        {
            /**
             * Info on struct for av_log
             * - set by avcodec_alloc_context
             */
            public IntPtr av_class; //  AVClass *av_class;

            [MarshalAs(UnmanagedType.I4)]
            public int log_level_offset;

            public AVMediaType codec_type; /* see CODEC_TYPE_xxx */

            public IntPtr codec; // AVCodec

            public AVCodecID codec_id; /* see CODEC_ID_xxx */

            //*
            //* fourcc(LSB first, so "ABCD" -> ('D'<<24) + ('C'<<16) + ('B'<<8) + 'A').
            //* this is used to workaround some encoder bugs
            //* - encoding: set by user, if not then the default based on codec_id will be used
            //* - decoding: set by user, will be converted to upper case by lavc during init

            [MarshalAs(UnmanagedType.U4)]
            public uint codec_tag;

            public IntPtr priv_data;

            /**
            * internal_buffers.
            * Don't touch, used by lavc default_get_buffer()
            */
            public IntPtr internal_buffer; // void* internal_buffer;

            /**
            * private data of the user, can be used to carry app specific stuff.
            * - encoding: set by user
            * - decoding: set by user
            */
            public IntPtr opaque;

            /**
             * the average bitrate.
             * - encoding: set by user. unused for constant quantizer encoding
             * - decoding: set by lavc. 0 or some bitrate if this info is available in the stream
             */
            [MarshalAs(UnmanagedType.I8)]
            public Int64 bit_rate;

            /**
             * number of bits the bitstream is allowed to diverge from the reference.
             *           the reference can be CBR (for CBR pass1) or VBR (for pass2)
             * - encoding: set by user. unused for constant quantizer encoding
             * - decoding: unused
             */
            [MarshalAs(UnmanagedType.I4)]
            public int bit_rate_tolerance;

            /**
            * global quality for codecs which cannot change it per frame.
            * this should be proportional to MPEG1/2/4 qscale.
            * - encoding: set by user.
            * - decoding: unused
            */
            [MarshalAs(UnmanagedType.I4)]
            public int global_quality;

            /**
            * - encoding: set by user.
            * - decoding: unused
            */
            [MarshalAs(UnmanagedType.I4)]
            public int compression_level;

            /**
             * CODEC_FLAG_*.
             * - encoding: set by user.
             * - decoding: set by user.
             */
            [MarshalAs(UnmanagedType.I4)]
            public int flags;

            /**
            * CODEC_FLAG2_*.
            * - encoding: set by user.
            * - decoding: set by user.
            */
            [MarshalAs(UnmanagedType.I4)]
            public int flags2;

            /**
             * some codecs need / can use extra-data like huffman tables.
             * mjpeg: huffman tables
             * rv10: additional flags
             * mpeg4: global headers (they can be in the bitstream or here)
             * the allocated memory should be FF_INPUT_BUFFER_PADDING_SIZE bytes larger
             * then extradata_size to avoid prolems if its read with the bitstream reader
             * the bytewise contents of extradata must not depend on the architecture or cpu endianness
             * - encoding: set/allocated/freed by lavc.
             * - decoding: set/allocated/freed by user.
             */
            public IntPtr extradata; // void* extradata;

            [MarshalAs(UnmanagedType.I4)]
            public int extradata_size;

            /**
            * this is the fundamental unit of time (in seconds) in terms
            * of which frame timestamps are represented. for fixed-fps content,
            * timebase should be 1/framerate and timestamp increments should be
            * identically 1.
            * - encoding: MUST be set by user
            * - decoding: set by lavc.
            */
            public AVRational time_base;

            /**
            * For some codecs, the time base is closer to the field rate than the frame rate.
            * Most notably, H.264 and MPEG-2 specify time_base as half of frame duration
            * if no telecine is used ...
            *
            * Set to time_base ticks per frame. Default 1, e.g., H.264/MPEG-2 set it to 2.
            */
            [MarshalAs(UnmanagedType.I4)]
            public int ticks_per_frame;

            /**
            * number of frames the decoded output will be delayed relative to
            * the encoded input.
            * - encoding: set by lavc.
            * - decoding: unused
            */
            [MarshalAs(UnmanagedType.I4)]
            public int delay;

            /* video only */
            /**
             * picture width / height.
             * - encoding: MUST be set by user.
             * - decoding: set by lavc.
             * Note, for compatibility its possible to set this instead of
             * coded_width/height before decoding
             */
            [MarshalAs(UnmanagedType.I4)]
            public int width;

            [MarshalAs(UnmanagedType.I4)]
            public int height;

            /**
             * bitsream width / height. may be different from width/height if lowres
             * or other things are used
             * - encoding: unused
             * - decoding: set by user before init if known, codec should override / dynamically change if needed
             */
            [MarshalAs(UnmanagedType.I4)]
            public int coded_width, coded_height;

            /**
             * the number of pictures in a group of pitures, or 0 for intra_only.
             * - encoding: set by user.
             * - decoding: unused
             */
            [MarshalAs(UnmanagedType.I4)]
            public int gop_size;

            /**
            * pixel format, see PIX_FMT_xxx.
            * - encoding: set by user.
            * - decoding: set by lavc.
            */
            public AVPixelFormat pix_fmt;

            /**
            * motion estimation algorithm used for video coding.
            * 1 (zero), 2 (full), 3 (log), 4 (phods), 5 (epzs), 6 (x1), 7 (hex),
            * 8 (umh), 9 (iter) [7, 8 are x264 specific, 9 is snow specific]
            * - encoding: MUST be set by user.
            * - decoding: unused
            */
            [MarshalAs(UnmanagedType.I4)]
            public int me_method;

            /**
            * if non NULL, 'draw_horiz_band' is called by the libavcodec
            * decoder to draw an horizontal band. It improve cache usage. Not
            * all codecs can do that. You must check the codec capabilities
            * before
            * - encoding: unused
            * - decoding: set by user.
            * @param height the height of the slice
            * @param y the y position of the slice
            * @param type 1->top field, 2->bottom field, 3->frame
            * @param offset offset into the AVFrame.data from which the slice should be read
            */
            [MarshalAs(UnmanagedType.FunctionPtr)]
            public DrawhorizBandCallback draw_horiz_band;

            /**
            * callback to negotiate the pixelFormat.
            * @param fmt is the list of formats which are supported by the codec,
            * its terminated by -1 as 0 is a valid format, the formats are ordered by quality
            * the first is allways the native one
            * @return the choosen format
            * - encoding: unused
            * - decoding: set by user, if not set then the native format will always be choosen
            */
            [MarshalAs(UnmanagedType.FunctionPtr)]
            public GetFormatCallback get_format;

            /**
            * maximum number of b frames between non b frames.
            * note: the output will be delayed by max_b_frames+1 relative to the input
            * - encoding: set by user.
            * - decoding: unused
            */
            [MarshalAs(UnmanagedType.I4)]
            public int max_b_frames;

            /**
             * qscale factor between ip and b frames.
             * - encoding: set by user.
             * - decoding: unused
             */
            [MarshalAs(UnmanagedType.R4)]
            public float b_quant_factor;

            /** obsolete FIXME remove */
            [MarshalAs(UnmanagedType.I4)]
            public int rc_strategy;

            [MarshalAs(UnmanagedType.I4)]
            public int b_frame_strategy;

            /**
            * qscale offset between ip and b frames.
            * if > 0 then the last p frame quantizer will be used (q= lastp_q*factor+offset)
            * if < 0 then normal ratecontrol will be done (q= -normal_q*factor+offset)
            * - encoding: set by user.
            * - decoding: unused
            */
            [MarshalAs(UnmanagedType.R4)]
            public float b_quant_offset;

            /**
            * if 1 the stream has a 1 frame delay during decoding.
            * - encoding: set by lavc
            * - decoding: set by lavc
            */
            [MarshalAs(UnmanagedType.I4)]
            public int has_b_frames;

            /**
            * 0-> h263 quant 1-> mpeg quant.
            * - encoding: set by user.
            * - decoding: unused
            */
            [MarshalAs(UnmanagedType.I4)]
            public int mpeg_quant;

            /**
            * qscale factor between p and i frames.
            * if > 0 then the last p frame quantizer will be used (q= lastp_q*factor+offset)
            * if < 0 then normal ratecontrol will be done (q= -normal_q*factor+offset)
            * - encoding: set by user.
            * - decoding: unused
            */
            [MarshalAs(UnmanagedType.R4)]
            public float i_quant_factor;

            /**
             * qscale offset between p and i frames.
             * - encoding: set by user.
             * - decoding: unused
             */
            [MarshalAs(UnmanagedType.R4)]
            public float i_quant_offset;

            /**
            * luminance masking (0-> disabled).
            * - encoding: set by user
            * - decoding: unused
            */
            [MarshalAs(UnmanagedType.R4)]
            public float lumi_masking;

            /**
             * temporary complexity masking (0-> disabled).
             * - encoding: set by user
             * - decoding: unused
             */
            [MarshalAs(UnmanagedType.R4)]
            public float temporal_cplx_masking;

            /**
             * spatial complexity masking (0-> disabled).
             * - encoding: set by user
             * - decoding: unused
             */
            [MarshalAs(UnmanagedType.R4)]
            public float spatial_cplx_masking;

            /**
             * p block masking (0-> disabled).
             * - encoding: set by user
             * - decoding: unused
             */
            [MarshalAs(UnmanagedType.R4)]
            public float p_masking;

            /**
             * darkness masking (0-> disabled).
             * - encoding: set by user
             * - decoding: unused
             */
            [MarshalAs(UnmanagedType.R4)]
            public float dark_masking;

            /**
            * slice count.
            * - encoding: set by lavc
            * - decoding: set by user (or 0)
            */
            [MarshalAs(UnmanagedType.I4)]
            public int slice_count;

            /**
             * slice offsets in the frame in bytes.
             * - encoding: set/allocated by lavc
             * - decoding: set/allocated by user (or NULL)
             */
            public IntPtr slice_offset; // int *slice_offset

            /**
            * sample aspect ratio (0 if unknown).
            * numerator and denominator must be relative prime and smaller then 256 for some video standards
            * - encoding: set by user.
            * - decoding: set by lavc.
            */
            public AVRational sample_aspect_ratio;

            /**
            * motion estimation compare function.
            * - encoding: set by user.
            * - decoding: unused
            */
            [MarshalAs(UnmanagedType.I4)]
            public int me_cmp;


            /**
             * subpixel motion estimation compare function.
             * - encoding: set by user.
             * - decoding: unused
             */
            [MarshalAs(UnmanagedType.I4)]
            public int me_sub_cmp;

            /**
             * macroblock compare function (not supported yet).
             * - encoding: set by user.
             * - decoding: unused
             */
            [MarshalAs(UnmanagedType.I4)]
            public int mb_cmp;

            /**
             * interlaced dct compare function
             * - encoding: set by user.
             * - decoding: unused
             */
            [MarshalAs(UnmanagedType.I4)]
            public int ildct_cmp;

            /**
             * ME diamond size & shape.
             * - encoding: set by user.
             * - decoding: unused
             */
            [MarshalAs(UnmanagedType.I4)]
            public int dia_size;

            /**
             * amount of previous MV predictors (2a+1 x 2a+1 square).
             * - encoding: set by user.
             * - decoding: unused
             */
            [MarshalAs(UnmanagedType.I4)]
            public int last_predictor_count;

            /**
             * pre pass for motion estimation.
             * - encoding: set by user.
             * - decoding: unused
             */
            [MarshalAs(UnmanagedType.I4)]
            public int pre_me;

            /**
             * motion estimation pre pass compare function.
             * - encoding: set by user.
             * - decoding: unused
             */
            [MarshalAs(UnmanagedType.I4)]
            public int me_pre_cmp;

            /**
             * ME pre pass diamond size & shape.
             * - encoding: set by user.
             * - decoding: unused
             */
            [MarshalAs(UnmanagedType.I4)]
            public int pre_dia_size;

            /**
             * subpel ME quality.
             * - encoding: set by user.
             * - decoding: unused
             */
            [MarshalAs(UnmanagedType.I4)]
            public int me_subpel_quality;

            /**
             * DTG active format information (additionnal aspect ratio
             * information only used in DVB MPEG2 transport streams). 0 if
             * not set.
             *
             * - encoding: unused.
             * - decoding: set by decoder
             */
            [MarshalAs(UnmanagedType.I4)]
            public int dtg_active_format;

            /**
             * Maximum motion estimation search range in subpel units.
             * if 0 then no limit
             *
             * - encoding: set by user.
             * - decoding: unused.
             */
            [MarshalAs(UnmanagedType.I4)]
            public int me_range;

            /**
             * intra quantizer bias.
             * - encoding: set by user.
             * - decoding: unused
             */
            [MarshalAs(UnmanagedType.I4)]
            public int intra_quant_bias;

            /**
             * inter quantizer bias.
             * - encoding: set by user.
             * - decoding: unused
             */
            [MarshalAs(UnmanagedType.I4)]
            public int inter_quant_bias;

            /**
            * slice flags
            * - encoding: unused
            * - decoding: set by user.
            */
            [MarshalAs(UnmanagedType.I4)]
            public int slice_flags;

            /**
             * XVideo Motion Acceleration
             * - encoding: forbidden
             * - decoding: set by decoder
             */
            [MarshalAs(UnmanagedType.I4)]
            public int xvmc_acceleration;

            /**
            * macroblock decision mode
            * - encoding: set by user.
            * - decoding: unused
            */
            [MarshalAs(UnmanagedType.I4)]
            public int mb_decision;

            /**
             * custom intra quantization matrix
             * - encoding: set by user, can be NULL
             * - decoding: set by lavc
             */
            public IntPtr intra_matrix; // uint16_t* intra_matrix;

            /**
             * custom inter quantization matrix
             * - encoding: set by user, can be NULL
             * - decoding: set by lavc
             */
            public IntPtr inter_matrix; // uint16_t* inter_matrix;

            /**
            * scene change detection threshold.
            * 0 is default, larger means fewer detected scene changes
            * - encoding: set by user.
            * - decoding: unused
            */
            [MarshalAs(UnmanagedType.I4)]
            public int scenechange_threshold;

            /**
            * noise reduction strength
            * - encoding: set by user.
            * - decoding: unused
            */
            [MarshalAs(UnmanagedType.I4)]
            public int noise_reduction;

            /**
            * Motion estimation threshold. under which no motion estimation is
            * performed, but instead the user specified motion vectors are used
            *
            * - encoding: set by user
            * - decoding: unused
            */
            [MarshalAs(UnmanagedType.I4)]
            public int me_threshold;

            /**
             * Macroblock threshold. under which the user specified macroblock types will be used
             * - encoding: set by user
             * - decoding: unused
             */
            [MarshalAs(UnmanagedType.I4)]
            public int mb_threshold;

            /**
             * precision of the intra dc coefficient - 8.
             * - encoding: set by user
             * - decoding: unused
             */
            [MarshalAs(UnmanagedType.I4)]
            public int intra_dc_precision;

            /**
            * number of macroblock rows at the top which are skipped.
            * - encoding: unused
            * - decoding: set by user
            */
            [MarshalAs(UnmanagedType.I4)]
            public int skip_top;

            /**
             * number of macroblock rows at the bottom which are skipped.
             * - encoding: unused
             * - decoding: set by user
             */
            [MarshalAs(UnmanagedType.I4)]
            public int skip_bottom;

            /**
            * border processing masking. raises the quantizer for mbs on the borders
            * of the picture.
            * - encoding: set by user
            * - decoding: unused
            */
            [MarshalAs(UnmanagedType.R4)]
            public float border_masking;

            /**
             * minimum MB lagrange multipler.
             * - encoding: set by user.
             * - decoding: unused
             */
            [MarshalAs(UnmanagedType.I4)]
            public int mb_lmin;

            /**
             * maximum MB lagrange multipler.
             * - encoding: set by user.
             * - decoding: unused
             */
            [MarshalAs(UnmanagedType.I4)]
            public int mb_lmax;

            /**
             *
             * - encoding: set by user.
             * - decoding: unused
             */
            [MarshalAs(UnmanagedType.I4)]
            public int me_penalty_compensation;

            /**
            *
            * - encoding: set by user.
            * - decoding: unused
            */
            [MarshalAs(UnmanagedType.I4)]
            public int bidir_refine;

            /**
             *
             * - encoding: set by user.
             * - decoding: unused
             */
            [MarshalAs(UnmanagedType.I4)]
            public int brd_scale;

            /**
            * minimum gop size
            * - encoding: set by user.
            * - decoding: unused
            */
            [MarshalAs(UnmanagedType.I4)]
            public int keyint_min;

            /**
             * number of reference frames
             * - encoding: set by user.
             * - decoding: unused
             */
            [MarshalAs(UnmanagedType.I4)]
            public int refs;

            /**
             * chroma qp offset from luma
             * - encoding: set by user.
             * - decoding: unused
             */
            [MarshalAs(UnmanagedType.I4)]
            public int chromaoffset;

            /**
            * multiplied by qscale for each frame and added to scene_change_score
            * - encoding: set by user.
            * - decoding: unused
            */
            [MarshalAs(UnmanagedType.I4)]
            public int scenechange_factor;

            /**
             *
             * note: value depends upon the compare functin used for fullpel ME
             * - encoding: set by user.
             * - decoding: unused
             */
            [MarshalAs(UnmanagedType.I4)]
            public int mv0_threshold;

            /**
             * adjusts sensitivity of b_frame_strategy 1
             * - encoding: set by user.
             * - decoding: unused
             */
            [MarshalAs(UnmanagedType.I4)]
            public int b_sensitivity;

            /**
            * Chromaticity coordinates of the source primaries.
            * - encoding: Set by user
            * - decoding: Set by libavcodec
            */
            public AVColorPrimaries color_primaries;

            /**
            * Color Transfer Characteristic.
            * - encoding: Set by user
            * - decoding: Set by libavcodec
            */
            public AVColorTransferCharacteristic color_trc;

            /**
             * YUV colorspace type.
             * - encoding: Set by user
             * - decoding: Set by libavcodec
             */
            public AVColorSpace colorspace;

            /**
             * MPEG vs JPEG YUV range.
             * - encoding: Set by user
             * - decoding: Set by libavcodec
             */
            public AVColorRange color_range;

            /**
             * This defines the location of chroma samples.
             * - encoding: Set by user
             * - decoding: Set by libavcodec
             */
            public AVChromaLocation chroma_sample_location;

            /**
            * Number of slices.
            * Indicates number of picture subdivisions. Used for parallelized
            * decoding.
            * - encoding: Set by user
            * - decoding: unused
            */
            [MarshalAs(UnmanagedType.I4)]
            public int slices;

            /** Field order
            * - encoding: set by libavcodec
            * - decoding: Set by user.
            */
            public AVFieldOrder field_order;

            /* audio only */
            [MarshalAs(UnmanagedType.I4)]
            public int sample_rate; // samples per sec

            [MarshalAs(UnmanagedType.I4)]
            public int channels;

            /**
             * audio sample format.
             * - encoding: set by user.
             * - decoding: set by lavc.
             */
            public SampleFormat sample_fmt;  // sample format, currenly unused

            /* the following data should not be initialized */
            /**
             * samples per packet. initialized when calling 'init'
             */
            [MarshalAs(UnmanagedType.I4)]
            public int frame_size;

            [MarshalAs(UnmanagedType.I4)]
            public int frame_number;   // audio or video frame number

            /**
            * number of bytes per packet if constant and known or 0
            * used by some WAV based audio codecs
            */
            [MarshalAs(UnmanagedType.I4)]
            public int block_align;

            /**
            * audio cutoff bandwidth (0 means "automatic") . Currently used only by FAAC
            * - encoding: set by user.
            * - decoding: unused
            */
            [MarshalAs(UnmanagedType.I4)]
            public int cutoff;

            /**
            * Audio channel layout.
            * - encoding: set by user.
            * - decoding: set by user, may be overwritten by libavcodec.
            */
            [MarshalAs(UnmanagedType.I8)]
            UInt64 channel_layout;

            /**
            * Request decoder to use this channel layout if it can (0 for default)
            * - encoding: unused
            * - decoding: Set by user.
            */
            [MarshalAs(UnmanagedType.I8)]
            UInt64 request_channel_layout;

            /**
            * Type of service that the audio stream conveys.
            * - encoding: Set by user.
            * - decoding: Set by libavcodec.
            */
            public AVAudioServiceType audio_service_type;

            /**
            * desired sample format
            * - encoding: Not used.
            * - decoding: Set by user.
            * Decoder will decode to this format if it can.
            */
            public AVSampleFormat request_sample_fmt;

            /**
            * called at the beginning of each frame to get a buffer for it.
            * if pic.reference is set then the frame will be read later by lavc
            * avcodec_align_dimensions() should be used to find the required width and
            * height, as they normally need to be rounded up to the next multiple of 16
            * - encoding: unused
            * - decoding: set by lavc, user can override
            */
            [MarshalAs(UnmanagedType.FunctionPtr)]
            public GetBufferCallback get_buffer2;

            /**
            * If non-zero, the decoded audio and video frames returned from
            * avcodec_decode_video2() and avcodec_decode_audio4() are reference-counted
            * and are valid indefinitely. The caller must free them with
            * av_frame_unref() when they are not needed anymore.
            * Otherwise, the decoded frames must not be freed by the caller and are
            * only valid until the next decode call.
            *
            * This is always automatically enabled if avcodec_receive_frame() is used.
            *
            * - encoding: unused
            * - decoding: set by the caller before avcodec_open2().
            */
            [MarshalAs(UnmanagedType.I4)]
            public int refcounted_frames;

            /* - encoding parameters */
            [MarshalAs(UnmanagedType.R4)]
            public float qcompress;  // amount of qscale change between easy & hard scenes (0.0-1.0)

            [MarshalAs(UnmanagedType.R4)]
            public float qblur;      // amount of qscale smoothing over time (0.0-1.0)

            /**
             * minimum quantizer.
             * - encoding: set by user.
             * - decoding: unused
             */
            [MarshalAs(UnmanagedType.I4)]
            public int qmin;

            /**
             * maximum quantizer.
             * - encoding: set by user.
             * - decoding: unused
             */
            [MarshalAs(UnmanagedType.I4)]
            public int qmax;

            /**
             * maximum quantizer difference etween frames.
             * - encoding: set by user.
             * - decoding: unused
             */
            [MarshalAs(UnmanagedType.I4)]
            public int max_qdiff;

            /**
            * ratecontrol qmin qmax limiting method.
            * 0-> clipping, 1-> use a nice continous function to limit qscale wthin qmin/qmax
            * - encoding: set by user.
            * - decoding: unused
            */
            [MarshalAs(UnmanagedType.R4)]
            public float rc_qsquish;

            [MarshalAs(UnmanagedType.R4)]
            float rc_qmod_amp;

            [MarshalAs(UnmanagedType.I4)]
            public int rc_qmod_freq;

            /**
            * decoder bitstream buffer size.
            * - encoding: set by user.
            * - decoding: unused
            */
            [MarshalAs(UnmanagedType.I4)]
            public int rc_buffer_size;

            [MarshalAs(UnmanagedType.I4)]
            public int rc_override_count;

            /**
            * ratecontrol override, see RcOverride.
            * - encoding: allocated/set/freed by user.
            * - decoding: unused
            */
            public IntPtr rc_override; // RcOverride* rc_override;

            /**
             * rate control equation.
             * - encoding: set by user
             * - decoding: unused
             */
            [MarshalAs(UnmanagedType.LPStr)]
            public String rc_eq; // char* rc_eq;

            /**
             * maximum bitrate.
             * - encoding: set by user.
             * - decoding: unused
             */
            [MarshalAs(UnmanagedType.I4)]
            public int rc_max_rate;

            /**
             * minimum bitrate.
             * - encoding: set by user.
             * - decoding: unused
             */
            [MarshalAs(UnmanagedType.I4)]
            public int rc_min_rate;

            [MarshalAs(UnmanagedType.R4)]
            public float rc_buffer_aggressivity;

            /**
             * initial complexity for pass1 ratecontrol.
             * - encoding: set by user.
             * - decoding: unused
             */
            [MarshalAs(UnmanagedType.R4)]
            public float rc_initial_cplx;

            /**
            * Ratecontrol attempt to use, at maximum, <value> of what can be used without an underflow.
            * - encoding: Set by user.
            * - decoding: unused.
            */
            [MarshalAs(UnmanagedType.R4)]
            public float rc_max_available_vbv_use;

            /**
            * Ratecontrol attempt to use, at least, <value> times the amount needed to prevent a vbv overflow.
            * - encoding: Set by user.
            * - decoding: unused.
            */
            [MarshalAs(UnmanagedType.R4)]
            public float rc_min_vbv_overflow_use;

            /**
            * number of bits which should be loaded into the rc buffer before decoding starts
            * - encoding: set by user.
            * - decoding: unused
            */
            [MarshalAs(UnmanagedType.I4)]
            public int rc_initial_buffer_occupancy;

            /**
            * coder type
            * - encoding: set by user.
            * - decoding: unused
            */
            [MarshalAs(UnmanagedType.I4)]
            public int coder_type;

            /**
             * context model
             * - encoding: set by user.
             * - decoding: unused
             */
            [MarshalAs(UnmanagedType.I4)]
            public int context_model;

            /**
            * minimum lagrange multipler
            * - encoding: set by user.
            * - decoding: unused
            */
            [MarshalAs(UnmanagedType.I4)]
            public int lmin;

            /**
             * maximum lagrange multipler
             * - encoding: set by user.
             * - decoding: unused
             */
            [MarshalAs(UnmanagedType.I4)]
            public int lmax;

            /**
            * frame skip threshold
            * - encoding: set by user
            * - decoding: unused
            */
            [MarshalAs(UnmanagedType.I4)]
            public int frame_skip_threshold;

            /**
             * frame skip factor
             * - encoding: set by user
             * - decoding: unused
             */
            [MarshalAs(UnmanagedType.I4)]
            public int frame_skip_factor;

            /**
             * frame skip exponent
             * - encoding: set by user
             * - decoding: unused
             */
            [MarshalAs(UnmanagedType.I4)]
            public int frame_skip_exp;

            /**
             * frame skip comparission function
             * - encoding: set by user.
             * - decoding: unused
             */
            [MarshalAs(UnmanagedType.I4)]
            public int frame_skip_cmp;

            /**
            * trellis RD quantization
            * - encoding: set by user.
            * - decoding: unused
            */
            [MarshalAs(UnmanagedType.I4)]
            public int trellis;

            /**
            * - encoding: set by user.
            * - decoding: unused.
            */
            [MarshalAs(UnmanagedType.I4)]
            public int min_prediction_order;

            /**
             * - encoding: set by user.
             * - decoding: unused.
             */
            [MarshalAs(UnmanagedType.I4)]
            public int max_prediction_order;

            [MarshalAs(UnmanagedType.I8)]
            public Int64 timecode_frame_start;

            /* The RTP callback: This function is called   */
            /* every time the encoder has a packet to send */
            /* Depends on the encoder if the data starts   */
            /* with a Start Code (it should) H.263 does.   */
            /* mb_nb contains the number of macroblocks    */
            /* encoded in the RTP payload                  */
            [MarshalAs(UnmanagedType.FunctionPtr)]
            public RtpCallback rtp_callback;

            /* The size of the RTP payload: the coder will  */
            /* do it's best to deliver a chunk with size    */
            /* below rtp_payload_size, the chunk will start */
            /* with a start code on some codecs like H.263  */
            /* This doesn't take account of any particular  */
            /* headers inside the transmited RTP payload    */
            [MarshalAs(UnmanagedType.I4)]
            public int rtp_payload_size;

            /* statistics, used for 2-pass encoding */
            [MarshalAs(UnmanagedType.I4)]
            public int mv_bits;

            [MarshalAs(UnmanagedType.I4)]
            public int header_bits;

            [MarshalAs(UnmanagedType.I4)]
            public int i_tex_bits;

            [MarshalAs(UnmanagedType.I4)]
            public int p_tex_bits;

            [MarshalAs(UnmanagedType.I4)]
            public int i_count;

            [MarshalAs(UnmanagedType.I4)]
            public int p_count;

            [MarshalAs(UnmanagedType.I4)]
            public int skip_count;

            [MarshalAs(UnmanagedType.I4)]
            public int misc_bits;

            /**
             * number of bits used for the previously encoded frame.
             * - encoding: set by lavc
             * - decoding: unused
             */
            [MarshalAs(UnmanagedType.I4)]
            public int frame_bits;

            /**
            * pass1 encoding statistics output buffer.
            * - encoding: set by lavc
            * - decoding: unused
            */
            [MarshalAs(UnmanagedType.LPStr)]
            public String stats_out; // char* stats_out

            /**
             * pass2 encoding statistics input buffer.
             * concatenated stuff from stats_out of pass1 should be placed here
             * - encoding: allocated/set/freed by user
             * - decoding: unused
             */
            [MarshalAs(UnmanagedType.LPStr)]
            public String stats_in;// char *stats_in

            /**
            * workaround bugs in encoders which sometimes cannot be detected automatically.
            * - encoding: set by user
            * - decoding: set by user
            */
            [MarshalAs(UnmanagedType.I4)]
            public int workaround_bugs;

            /**
            * strictly follow the std (MPEG4, ...).
            * - encoding: set by user
            * - decoding: unused
            */
            [MarshalAs(UnmanagedType.I4)]
            public int strict_std_compliance;

            /**
            * error concealment flags.
            * - encoding: unused
            * - decoding: set by user
            */
            [MarshalAs(UnmanagedType.I4)]
            public int error_concealment;

            /**
            * debug.
            * - encoding: set by user.
            * - decoding: set by user.
            */
            [MarshalAs(UnmanagedType.I4)]
            public int debug;

            /**
             * debug.
             * - encoding: set by user.
             * - decoding: set by user.
             */
            [MarshalAs(UnmanagedType.I4)]
            public int debug_mv;

            /**
            * Error recognition; may misdetect some more or less valid parts as errors.
            * - encoding: unused
            * - decoding: Set by user.
            */
            [MarshalAs(UnmanagedType.I4)]
            public int err_recognition;

            /**
            * opaque 64-bit number (generally a PTS) that will be reordered and
            * output in AVFrame.reordered_opaque
            * - encoding: unused
            * - decoding: Set by user.
            */
            [MarshalAs(UnmanagedType.I8)]
            public Int64 reordered_opaque;

            /**
            * Hardware accelerator in use
            * - encoding: unused.
            * - decoding: Set by libavcodec
            */
            public AVHWAccel hwaccel;

            /**
            * Hardware accelerator context.
            * For some hardware accelerators, a global context needs to be
            * provided by the user. In that case, this holds display-dependent
            * data FFmpeg cannot instantiate itself. Please refer to the
            * FFmpeg HW accelerator documentation to know how to fill this
            * is. e.g. for VA API, this is a struct vaapi_context.
            * - encoding: unused
            * - decoding: Set by user
            */
            [MarshalAs(UnmanagedType.FunctionPtr)]
            public GetHWAccelContext hwaccel_context;

            /**
            * error.
            * - encoding: set by lavc if flags&CODEC_FLAG_PSNR
            * - decoding: unused
            */
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public Int64[] error;

            /**
            * dct algorithm, see FF_DCT_* below.
            * - encoding: set by user
            * - decoding: unused
            */
            [MarshalAs(UnmanagedType.I4)]
            public int dct_algo;

            /**
             * idct algorithm, see FF_IDCT_* below.
             * - encoding: set by user
             * - decoding: set by user
             */
            [MarshalAs(UnmanagedType.I4)]
            public int idct_algo;

            /**
            * bits per sample/pixel from the demuxer (needed for huffyuv).
            * - encoding: Set by libavcodec.
            * - decoding: Set by user.
            */
            [MarshalAs(UnmanagedType.I4)]
            public int bits_per_coded_sample;

            /**
             * Bits per sample/pixel of internal libavcodec pixel/sample format.
             * - encoding: set by user.
             * - decoding: set by libavcodec.
             */
            [MarshalAs(UnmanagedType.I4)]
            public int bits_per_raw_sample;

            /**
            * low resolution decoding. 1-> 1/2 size, 2->1/4 size
            * - encoding: unused
            * - decoding: set by user
            */
            [MarshalAs(UnmanagedType.I4)]
            public int lowres;

            /**
            * the picture in the bitstream.
            * - encoding: set by lavc
            * - decoding: set by lavc
            */
            public IntPtr coded_frame; // AVFrame* coded_frame;

            /**
            * Thread count.
            * is used to decide how many independant tasks should be passed to execute()
            * - encoding: set by user
            * - decoding: set by user
            */
            [MarshalAs(UnmanagedType.I4)]
            public int thread_count;

            /**
            * Which multithreading methods to use.
            * Use of FF_THREAD_FRAME will increase decoding delay by one frame per thread,
            * so clients which cannot provide future frames should not use it.
            *
            * - encoding: Set by user, otherwise the default is used.
            * - decoding: Set by user, otherwise the default is used.
            */
            [MarshalAs(UnmanagedType.I4)]
            public int thread_type;

            /**
            * Which multithreading methods are in use by the codec.
            * - encoding: Set by libavcodec.
            * - decoding: Set by libavcodec.
            */
            [MarshalAs(UnmanagedType.I4)]
            public int active_thread_type;

            /**
             * Set by the client if its custom get_buffer() callback can be called
             * synchronously from another thread, which allows faster multithreaded decoding.
             * draw_horiz_band() will be called from other threads regardless of this setting.
             * Ignored if the default get_buffer() is used.
             * - encoding: Set by user.
             * - decoding: Set by user.
             */
            [MarshalAs(UnmanagedType.I4)]
            public int thread_safe_callbacks;

            /**
            * the codec may call this to execute several independant things. it will return only after
            * finishing all tasks, the user may replace this with some multithreaded implementation, the
            * default implementation will execute the parts serially
            * @param count the number of things to execute
            * - encoding: set by lavc, user can override
            * - decoding: set by lavc, user can override
            */
            [MarshalAs(UnmanagedType.FunctionPtr)]
            public ExecuteCallback execute;

            //* The codec may call this to execute several independent things.
            //* It will return only after finishing all tasks.
            //* The user may replace this with some multithreaded implementation,
            //* the default implementation will execute the parts serially.
            //* Also see avcodec_thread_init and e.g.the --enable-pthread configure option.
            //* @param c context passed also to func
            //* @param count the number of things to execute
            //* @param arg2 argument passed unchanged to func
            //* @param ret return values of executed functions, must have space for "count" values.May be NULL.
            //* @param func function that will be called count times, with jobnr from 0 to count-1.
            //*             threadnr will be in the range 0 to c->thread_count-1 < MAX_THREADS and so that no
            //* two instances of func executing at the same time will have the same threadnr.
            //* @return always 0 currently, but code should handle a future improvement where when any call to func
            //* returns < 0 no further calls to func may be done and< 0 is returned.
            //* - encoding: Set by libavcodec, user can override.
            //* - decoding: Set by libavcodec, user can override.

            [MarshalAs(UnmanagedType.FunctionPtr)]
            public Execute2Callback execute2;

            /**
            * noise vs. sse weight for the nsse comparsion function.
            * - encoding: set by user
            * - decoding: unused
            */
            [MarshalAs(UnmanagedType.I4)]
            public int nsse_weight;

            /**
             * profile
             * - encoding: set by user
             * - decoding: set by lavc
             */
            [MarshalAs(UnmanagedType.I4)]
            public int profile;

            /**
            * level
            * - encoding: set by user
            * - decoding: set by lavc
            */
            [MarshalAs(UnmanagedType.I4)]
            public int level;

            /**
             *
             * - encoding: unused
             * - decoding: set by user.
             */
            AVDiscard skip_loop_filter;

            /**
            * Skip IDCT/dequantization for selected frames.
            * - encoding: unused
            * - decoding: Set by user.
            */
            AVDiscard skip_idct;

            /**
             *
             * - encoding: unused
             * - decoding: set by user.
             */
            AVDiscard skip_frame;

            /**
            * Header containing style information for text subtitles.
            * For SUBTITLE_ASS subtitle type, it should contain the whole ASS
            * [Script Info] and [V4+ Styles] section, plus the [Events] line and
            * the Format line following. It shouldn't include any Dialogue line.
            * - encoding: Set/allocated/freed by user (before avcodec_open2())
            * - decoding: Set/allocated/freed by libavcodec (by avcodec_open2())
            */
            [MarshalAs(UnmanagedType.U1)]
            public byte subtitle_header;

            [MarshalAs(UnmanagedType.I4)]
            public int subtitle_header_size;

            /**
            * simulates errors in the bitstream to test error concealment.
            * - encoding: set by user.
            * - decoding: unused.
            */
            [MarshalAs(UnmanagedType.I4)]
            public int error_rate;

            /**
            * VBV delay coded in the last frame (in periods of a 27 MHz clock).
            * Used for compliant TS muxing.
            * - encoding: Set by libavcodec.
            * - decoding: unused.
            * @deprecated this value is now exported as a part of
            * AV_PKT_DATA_CPB_PROPERTIES packet side data
            */
            [MarshalAs(UnmanagedType.U8)]
            public UInt64 vbv_delay;

            /**
            * Encoding only and set by default. Allow encoders to output packets
            * that do not contain any encoded data, only side data.
            *
            * Some encoders need to output such packets, e.g. to update some stream
            * parameters at the end of encoding.
            *
            * @deprecated this field disables the default behaviour and
            *             it is kept only for compatibility.
            */
            [MarshalAs(UnmanagedType.I4)]
            public int side_data_only_packets;

            /**
            * Audio only. The number of "priming" samples (padding) inserted by the
            * encoder at the beginning of the audio. I.e. this number of leading
            * decoded samples must be discarded by the caller to get the original audio
            * without leading padding.
            *
            * - decoding: unused
            * - encoding: Set by libavcodec. The timestamps on the output packets are
            *             adjusted by the encoder so that they always refer to the
            *             first sample of the data actually contained in the packet,
            *             including any added padding.  E.g. if the timebase is
            *             1/samplerate and the timestamp of the first input sample is
            *             0, the timestamp of the first output packet will be
            *             -initial_padding.
            */
            [MarshalAs(UnmanagedType.I4)]
            public int initial_padding;

            /**
            * - decoding: For codecs that store a framerate value in the compressed
            *             bitstream, the decoder may export it here. { 0, 1} when
            *             unknown.
            * - encoding: May be used to signal the framerate of CFR content to an
            *             encoder.
            */
            public AVRational framerate;

            /**
            * Nominal unaccelerated pixel format, see AV_PIX_FMT_xxx.
            * - encoding: unused.
            * - decoding: Set by libavcodec before calling get_format()
            */
            public AVPixelFormat sw_pix_fmt;

            /**
            * Timebase in which pkt_dts/pts and AVPacket.dts/pts are.
            * - encoding unused.
            * - decoding set by user.
            */
            public AVRational pkt_timebase;

            /**
            * AVCodecDescriptor
            * - encoding: unused.
            * - decoding: set by libavcodec.
            */
            public IntPtr codec_descriptor;

            /**
            * Current statistics for PTS correction.
            * - decoding: maintained and used by libavcodec, not intended to be used by user apps
            * - encoding: unused
            */
            [MarshalAs(UnmanagedType.I8)]
            public Int64 pts_correction_num_faulty_pts; /// Number of incorrect PTS values so far

            [MarshalAs(UnmanagedType.I8)]
            public Int64 pts_correction_num_faulty_dts; /// Number of incorrect DTS values so far

            [MarshalAs(UnmanagedType.I8)]
            public Int64 pts_correction_last_pts;       /// PTS of the last frame

            [MarshalAs(UnmanagedType.I8)]
            public Int64 pts_correction_last_dts;       /// DTS of the last frame

            /**
            * Character encoding of the input subtitles file.
            * - decoding: set by user
            * - encoding: unused
            */
            [MarshalAs(UnmanagedType.LPStr)]
            public String sub_charenc;// char *sub_charenc;

            /**
            * Subtitles character encoding mode. Formats or codecs might be adjusting
            * this setting (if they are doing the conversion themselves for instance).
            * - decoding: set by libavcodec
            * - encoding: unused
            */
            [MarshalAs(UnmanagedType.I4)]
            public int sub_charenc_mode;

            /**
            * Skip processing alpha if supported by codec.
            * Note that if the format uses pre-multiplied alpha (common with VP6,
            * and recommended due to better video quality/compression)
            * the image will look as if alpha-blended onto a black background.
            * However for formats that do not use pre-multiplied alpha
            * there might be serious artefacts (though e.g. libswscale currently
            * assumes pre-multiplied alpha anyway).
            *
            * - decoding: set by user
            * - encoding: unused
            */
            [MarshalAs(UnmanagedType.I4)]
            public int skip_alpha;

            /**
             * Number of samples to skip after a discontinuity
             * - decoding: unused
             * - encoding: set by libavcodec
             */
            [MarshalAs(UnmanagedType.I4)]
            public int seek_preroll;

            /**
            * custom intra quantization matrix
            * - encoding: Set by user, can be NULL.
            * - decoding: unused.
            */
            [MarshalAs(UnmanagedType.U2)]
            public UInt16 chroma_intra_matrix;

            /**
             * dump format separator.
             * can be ", " or "\n      " or anything else
             * - encoding: Set by user.
             * - decoding: Set by user.
             */
             [MarshalAs(UnmanagedType.U1)]
            public byte dump_separator;

            /**
             * ',' separated list of allowed decoders.
             * If NULL then all are allowed
             * - encoding: unused
             * - decoding: set by user
             */
            [MarshalAs(UnmanagedType.LPStr)]
            public String codec_whitelist;

            /*
            * Properties of the stream that gets decoded
            * - encoding: unused
            * - decoding: set by libavcodec
            */
            [MarshalAs(UnmanagedType.U4)]
            public UInt32 properties;

            /**
            * Additional data associated with the entire coded stream.
            *
            * - decoding: unused
            * - encoding: may be set by libavcodec after avcodec_open2().
            */
            public IntPtr coded_side_data;

            [MarshalAs(UnmanagedType.I4)]
            public int nb_coded_side_data;

            /**
            * A reference to the AVHWFramesContext describing the input (for encoding)
            * or output (decoding) frames. The reference is set by the caller and
            * afterwards owned (and freed) by libavcodec - it should never be read by
            * the caller after being set.
            *
            * - decoding: This field should be set by the caller from the get_format()
            *             callback. The previous reference (if any) will always be
            *             unreffed by libavcodec before the get_format() call.
            *
            *             If the default get_buffer2() is used with a hwaccel pixel
            *             format, then this AVHWFramesContext will be used for
            *             allocating the frame buffers.
            *
            * - encoding: For hardware encoders configured to use a hwaccel pixel
            *             format, this field should be set by the caller to a reference
            *             to the AVHWFramesContext describing input frames.
            *             AVHWFramesContext.format must be equal to
            *             AVCodecContext.pix_fmt.
            *
            *             This field should be set before avcodec_open2() is called.
            */
            public IntPtr hw_frames_ctx;

            /**
            * Control the form of AVSubtitle.rects[N]->ass
            * - decoding: set by user
            * - encoding: unused
            */
            [MarshalAs(UnmanagedType.I4)]
            public int sub_text_format;

            /**
            * Audio only. The amount of padding (in samples) appended by the encoder to
            * the end of the audio. I.e. this number of decoded samples must be
            * discarded by the caller from the end of the stream to get the original
            * audio without any trailing padding.
            *
            * - decoding: unused
            * - encoding: unused
            */
            [MarshalAs(UnmanagedType.I4)]
            public int trailing_padding;

            /**
            * The number of pixels per image to maximally accept.
            *
            * - decoding: set by user
            * - encoding: set by user
            */
            [MarshalAs(UnmanagedType.I8)]
            public Int64 max_pixels;

            /**
            * A reference to the AVHWDeviceContext describing the device which will
            * be used by a hardware encoder/decoder.  The reference is set by the
            * caller and afterwards owned (and freed) by libavcodec.
            *
            * This should be used if either the codec device does not require
            * hardware frames or any that are used are to be allocated internally by
            * libavcodec.  If the user wishes to supply any of the frames used as
            * encoder input or decoder output then hw_frames_ctx should be used
            * instead.  When hw_frames_ctx is set in get_format() for a decoder, this
            * field will be ignored while decoding the associated stream segment, but
            * may again be used on a following one after another get_format() call.
            *
            * For both encoders and decoders this field should be set before
            * avcodec_open2() is called and must not be written to thereafter.
            *
            * Note that some decoders may require this field to be set initially in
            * order to support hw_frames_ctx at all - in that case, all frames
            * contexts used must be created on the same device.
            */
            public IntPtr hw_device_ctx;

            /**
            * Bit set of AV_HWACCEL_FLAG_* flags, which affect hardware accelerated
            * decoding (if active).
            * - encoding: unused
            * - decoding: Set by user (either before avcodec_open2(), or in the
            *             AVCodecContext.get_format callback)
            */
            [MarshalAs(UnmanagedType.I4)]
            public int hwaccel_flags;

            /**
             * Video decoding only. Certain video codecs support cropping, meaning that
             * only a sub-rectangle of the decoded frame is intended for display.  This
             * option controls how cropping is handled by libavcodec.
             *
             * When set to 1 (the default), libavcodec will apply cropping internally.
             * I.e. it will modify the output frame width/height fields and offset the
             * data pointers (only by as much as possible while preserving alignment, or
             * by the full amount if the AV_CODEC_FLAG_UNALIGNED flag is set) so that
             * the frames output by the decoder refer only to the cropped area. The
             * crop_* fields of the output frames will be zero.
             *
             * When set to 0, the width/height fields of the output frames will be set
             * to the coded dimensions and the crop_* fields will describe the cropping
             * rectangle. Applying the cropping is left to the caller.
             *
             * @warning When hardware acceleration with opaque output frames is used,
             * libavcodec is unable to apply cropping from the top/left border.
             *
             * @note when this option is set to zero, the width/height fields of the
             * AVCodecContext and output AVFrames have different meanings. The codec
             * context fields store display dimensions (with the coded dimensions in
             * coded_width/height), while the frame fields store the coded dimensions
             * (with the display dimensions being determined by the crop_* fields).
             */
            [MarshalAs(UnmanagedType.I4)]
            public int apply_cropping;
        };

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int InitThreadCopyCallback(AVCodecContext pAVCodecContext);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int UpdateThreadContextCallback(AVCodecContext dst, AVCodecContext src);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void InitStaticDataCallback(AVCodec codec);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int InitCallback(AVCodecContext pAVCodecContext);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int EncodeSubCallback(AVCodecContext pAVCodecContext, IntPtr buf, int buf_size, IntPtr pAVSubtitle);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int Encode2Callback(AVCodecContext avctx, AVPacket avpkt, AVFrame frame, ref int got_pkt_ptr);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int DecodeCallback(AVCodecContext avctx, IntPtr outdata, ref int outdata_size, AVPacket avpkt);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int CloseCallback(AVCodecContext avctx);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int SendFrameCallback(AVCodecContext avctx, AVFrame frame);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int ReceivePacketCallback(AVCodecContext avctx, AVPacket avpkt);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int ReceiveFrameCallback(AVCodecContext avctx, AVFrame frame);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void FlushCallback(AVCodecContext avctx);

        [StructLayout(LayoutKind.Sequential)]
        public class AVCodec
        {
            [MarshalAs(UnmanagedType.LPStr)]
            public String name;

            /**
            * Descriptive name for the codec, meant to be more human readable than name.
            * You should use the NULL_IF_CONFIG_SMALL() macro to define it.
            */
            [MarshalAs(UnmanagedType.LPStr)]
            public String long_name;

            public AVMediaType type;

            public AVCodecID id;

            [MarshalAs(UnmanagedType.I4)]
            public int capabilities;

            // array of supported framerates, or NULL if any, array is terminated by {0,0}
            public IntPtr supported_framerates;

            // array of supported pixel formats, or NULL if unknown, array is terminanted by -1
            public IntPtr pix_fmts; // enum PixelFormat *pix_fmts

            public IntPtr supported_samplerates;       //< array of supported audio samplerates, or NULL if unknown, array is terminated by 0

            public IntPtr sample_fmts;          //< array of supported sample formats, or NULL if unknown, array is terminated by -1
            public IntPtr channel_layouts;      //< array of support channel layouts, or NULL if unknown. array is terminated by 0

            [MarshalAs(UnmanagedType.U1)]
            public byte max_lowres;             //< maximum value for lowres supported by the decoder

            public IntPtr priv_class;           //< AVClass for the private context
            public IntPtr profiles;             //< array of recognized profiles, or NULL if unknown, array is terminated by {FF_PROFILE_UNKNOWN}

            [MarshalAs(UnmanagedType.I4)]
            public int priv_data_size;

            //#if LIBAVCODEC_VERSION_INT < ((50<<16)+(0<<8)+0)
            //    void *dummy; // FIXME remove next time we break binary compatibility
            //#endif
            public IntPtr next; // AVCodec *next

            /**
            * @name Frame-level threading support functions
            * @{
            */
            /**
             * If defined, called on thread contexts when they are created.
             * If the codec allocates writable tables in init(), re-allocate them here.
             * priv_data will be set to a copy of the original.
             */
            //[MarshalAs(UnmanagedType.FunctionPtr)]
            public InitThreadCopyCallback init_thread_copy;

            /**
            * Copy necessary context variables from a previous thread context to the current one.
            * If not defined, the next thread will start automatically; otherwise, the codec
            * must call ff_thread_finish_setup().
            *
            * dst and src will (rarely) point to the same context, in which case memcpy should be skipped.
            */
            //[MarshalAs(UnmanagedType.FunctionPtr)]
            public UpdateThreadContextCallback update_thread_context;

            /**
            * Private codec-specific defaults.
            */
            public IntPtr defaults;

            /**
            * Initialize codec static data, called from avcodec_register().
            */
            //[MarshalAs(UnmanagedType.FunctionPtr)]
            public InitStaticDataCallback init_static_data;

            //[MarshalAs(UnmanagedType.FunctionPtr)]
            public InitCallback init;

            //[MarshalAs(UnmanagedType.FunctionPtr)]
            public EncodeSubCallback encode_sub;

            //[MarshalAs(UnmanagedType.FunctionPtr)]
            public Encode2Callback encode2;

            //[MarshalAs(UnmanagedType.FunctionPtr)]
            public DecodeCallback decode;

            //[MarshalAs(UnmanagedType.FunctionPtr)]
            public CloseCallback close;

            //[MarshalAs(UnmanagedType.FunctionPtr)]
            public SendFrameCallback send_frame;

            //[MarshalAs(UnmanagedType.FunctionPtr)]
            public ReceivePacketCallback receive_packet;

            //[MarshalAs(UnmanagedType.FunctionPtr)]
            public ReceiveFrameCallback receive_frame;

            //[MarshalAs(UnmanagedType.FunctionPtr)]
            public FlushCallback flush;

            /**
            * Internal codec capabilities.
            * See FF_CODEC_CAP_* in internal.h
            */
            [MarshalAs(UnmanagedType.I4)]
            public int caps_internal;

            /**
            * Decoding only, a comma-separated list of bitstream filters to apply to
            * packets before decoding.
            */
            [MarshalAs(UnmanagedType.LPStr)]
            public String bsfs;
        };

        public delegate int HWAccelAllocFrame(AVCodecContext avctx, AVFrame pAVFrame);

        public delegate int HWAccelStartFrame(AVCodecContext avctx, IntPtr buf, UInt32 buf_size);

        public delegate int HWAccelDecodeSlice(AVCodecContext avctx, IntPtr buf, UInt32 buf_size);

        public delegate int HWAccelEndFrame(AVCodecContext avctx);

        public delegate void HWAccelDecodeMB(IntPtr pMpegEncContext);

        public delegate int HWAccelInit(AVCodecContext avctx);

        public delegate int HWAccelUninit(AVCodecContext avctx);

        [StructLayout(LayoutKind.Sequential)]
        public class AVHWAccel
        {
            /**
             * Name of the hardware accelerated codec.
             * The name is globally unique among encoders and among decoders (but an
             * encoder and a decoder can share the same name).
             */
            [MarshalAs(UnmanagedType.LPStr)]
            public String name;

            /**
             * Type of codec implemented by the hardware accelerator.
             *
             * See AVMEDIA_TYPE_xxx
             */
            public AVMediaType type;

            /**
             * Codec implemented by the hardware accelerator.
             *
             * See AV_CODEC_ID_xxx
             */
            public AVCodecID id;

            /**
             * Supported pixel format.
             *
             * Only hardware accelerated formats are supported here.
             */
            public AVPixelFormat pix_fmt;

            /**
             * Hardware accelerated codec capabilities.
             * see HWACCEL_CODEC_CAP_*
             */
            [MarshalAs(UnmanagedType.I4)]
            public int capabilities;

            /*****************************************************************
             * No fields below this line are part of the public API. They
             * may not be used outside of libavcodec and can be changed and
             * removed at will.
             * New public fields should be added right above.
             *****************************************************************
             */
            public AVHWAccel next;

            /**
             * Allocate a custom buffer
             */
            [MarshalAs(UnmanagedType.FunctionPtr)]
            public HWAccelAllocFrame alloc_frame;

            /**
             * Called at the beginning of each frame or field picture.
             *
             * Meaningful frame information (codec specific) is guaranteed to
             * be parsed at this point. This function is mandatory.
             *
             * Note that buf can be NULL along with buf_size set to 0.
             * Otherwise, this means the whole frame is available at this point.
             *
             * @param avctx the codec context
             * @param buf the frame data buffer base
             * @param buf_size the size of the frame in bytes
             * @return zero if successful, a negative value otherwise
             */
            [MarshalAs(UnmanagedType.FunctionPtr)]
            public HWAccelStartFrame start_frame;

            /**
             * Callback for each slice.
             *
             * Meaningful slice information (codec specific) is guaranteed to
             * be parsed at this point. This function is mandatory.
             * The only exception is XvMC, that works on MB level.
             *
             * @param avctx the codec context
             * @param buf the slice data buffer base
             * @param buf_size the size of the slice in bytes
             * @return zero if successful, a negative value otherwise
             */
            [MarshalAs(UnmanagedType.FunctionPtr)]
            public HWAccelDecodeSlice decode_slice;

            /**
             * Called at the end of each frame or field picture.
             *
             * The whole picture is parsed at this point and can now be sent
             * to the hardware accelerator. This function is mandatory.
             *
             * @param avctx the codec context
             * @return zero if successful, a negative value otherwise
             */
            [MarshalAs(UnmanagedType.FunctionPtr)]
            public HWAccelEndFrame end_frame;

            /**
             * Size of per-frame hardware accelerator private data.
             *
             * Private data is allocated with av_mallocz() before
             * AVCodecContext.get_buffer() and deallocated after
             * AVCodecContext.release_buffer().
             */
            [MarshalAs(UnmanagedType.I4)]
            public int frame_priv_data_size;

            /**
             * Called for every Macroblock in a slice.
             *
             * XvMC uses it to replace the ff_mpv_reconstruct_mb().
             * Instead of decoding to raw picture, MB parameters are
             * stored in an array provided by the video driver.
             *
             * @param s the mpeg context
             */
            [MarshalAs(UnmanagedType.FunctionPtr)]
            public HWAccelDecodeMB decode_mb;

            /**
             * Initialize the hwaccel private data.
             *
             * This will be called from ff_get_format(), after hwaccel and
             * hwaccel_context are set and the hwaccel private data in AVCodecInternal
             * is allocated.
             */
            [MarshalAs(UnmanagedType.FunctionPtr)]
            public HWAccelInit init;

            /**
             * Uninitialize the hwaccel private data.
             *
             * This will be called from get_format() or avcodec_close(), after hwaccel
             * and hwaccel_context are already uninitialized.
             */
            [MarshalAs(UnmanagedType.FunctionPtr)]
            public HWAccelUninit uninit;

            /**
             * Size of the private data to allocate in
             * AVCodecInternal.hwaccel_priv_data.
             */
            [MarshalAs(UnmanagedType.I4)]
            public int priv_data_size;

            /**
             * Internal hwaccel capabilities.
             */
            [MarshalAs(UnmanagedType.I4)]
            public int caps_internal;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct AVPicture
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            IntPtr[] data; // uint8_t *data[4]
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            int[] linesize;
        };

        [StructLayout(LayoutKind.Sequential)]
        public struct AVPaletteControl
        {
            /* demuxer sets this to 1 to indicate the palette has changed;
             * decoder resets to 0 */
            [MarshalAs(UnmanagedType.I4)]
            public int palette_changed;

            /* 4-byte ARGB palette entries, stored in native byte order; note that
             * the individual palette components should be on a 8-bit scale; if
             * the palette data comes from a IBM VGA native format, the component
             * data is probably 6 bits in size and needs to be scaled */
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = AVPALETTE_COUNT)]
            public uint[] palette;
        };

        [StructLayout(LayoutKind.Sequential)]
        public struct AVSubtitleRect
        {
            [MarshalAs(UnmanagedType.U2)]
            public ushort x;

            [MarshalAs(UnmanagedType.U2)]
            public ushort y;

            [MarshalAs(UnmanagedType.U2)]
            public ushort w;

            [MarshalAs(UnmanagedType.U2)]
            public ushort h;

            [MarshalAs(UnmanagedType.U2)]
            public ushort nb_colors;

            [MarshalAs(UnmanagedType.I4)]
            public int linesize;

            public IntPtr bitmap; // uint8_t *bitmap;
        };

        [StructLayout(LayoutKind.Sequential)]
        public struct AVSubtitle
        {
            [MarshalAs(UnmanagedType.U2)]
            public ushort format;  /* 0 = graphics */

            [MarshalAs(UnmanagedType.U4)]
            public uint start_display_time; /* relative to packet pts, in ms */

            [MarshalAs(UnmanagedType.U4)]
            public uint end_display_time; /* relative to packet pts, in ms */

            [MarshalAs(UnmanagedType.U4)]
            public uint num_rects;

            public IntPtr rects; // AVSubtitleRect *rects;
        };

        [StructLayout(LayoutKind.Sequential)]
        public struct AVCodecParserContext
        {
            public IntPtr priv_data;

            public IntPtr parser; // AVCodecParser *parser

            [MarshalAs(UnmanagedType.I8)]
            public Int64 frame_offset; // offset of the current frame 

            [MarshalAs(UnmanagedType.I8)]
            public Int64 cur_offset; // current offset incremented by each av_parser_parse()

            [MarshalAs(UnmanagedType.I8)]
            public Int64 last_frame_offset; // offset of the last frame

            /* video info */
            [MarshalAs(UnmanagedType.I4)]
            public int pict_type; /* XXX: put it back in AVCodecContext */

            [MarshalAs(UnmanagedType.I4)]
            public int repeat_pict; /* XXX: put it back in AVCodecContext */

            [MarshalAs(UnmanagedType.I8)]
            public Int64 pts;     /* pts of the current frame */

            [MarshalAs(UnmanagedType.I8)]
            public Int64 dts;     /* dts of the current frame */

            /* private data */
            [MarshalAs(UnmanagedType.I8)]
            public Int64 last_pts;

            [MarshalAs(UnmanagedType.I8)]
            public Int64 last_dts;

            [MarshalAs(UnmanagedType.I4)]
            public int fetch_timestamp;

            [MarshalAs(UnmanagedType.I4)]
            public int cur_frame_start_index;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = AV_PARSER_PTS_NB)]
            public Int64[] cur_frame_offset;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = AV_PARSER_PTS_NB)]
            public Int64[] cur_frame_pts;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = AV_PARSER_PTS_NB)]
            public Int64[] cur_frame_dts;

            [MarshalAs(UnmanagedType.I4)]
            public int flags;
        };

        public delegate int ParaerInitCallback(IntPtr pAVCodecParserContext);

        public delegate int ParserParseCallback(IntPtr pAVCodecParserContext,
                                                IntPtr pAVCodecContext,
                                                [MarshalAs(UnmanagedType.LPArray)]IntPtr[] poutbuf,
                                                ref int poutbuf_size,
                                                IntPtr buf, int buf_size);

        public delegate void ParserCloseCallback(IntPtr pAVcodecParserContext);

        public delegate int SplitCallback(IntPtr pAVCodecContext, IntPtr buf, int buf_size);

        [StructLayout(LayoutKind.Sequential)]
        public struct AVCodecParser
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public int[] codec_ids; /* several codec IDs are permitted */

            [MarshalAs(UnmanagedType.I4)]
            public int priv_data_size;

            [MarshalAs(UnmanagedType.FunctionPtr)]
            public ParaerInitCallback parser_init;

            [MarshalAs(UnmanagedType.FunctionPtr)]
            public ParserParseCallback parser_parse;

            [MarshalAs(UnmanagedType.FunctionPtr)]
            public ParserCloseCallback parser_close;

            [MarshalAs(UnmanagedType.FunctionPtr)]
            public SplitCallback split;

            public IntPtr next;
        };

        [StructLayout(LayoutKind.Sequential)]
        public struct AVBitStreamFilterContext
        {
            public IntPtr priv_data;
            public IntPtr filter; // AVBitStreamFilter *filter
            public IntPtr parser; // AVCodecParserContext *parser
            public IntPtr next; // AVBitStreamFilterContext *next
        };

        public delegate int FilterCallback(IntPtr pAVBitStreamFilterContext,
                                        IntPtr pAVCodecContext,
                                        [MarshalAs(UnmanagedType.LPStr)]String args,
                                        [MarshalAs(UnmanagedType.LPArray)]IntPtr[] poutbuf, ref int poutbuf_size,
                                        IntPtr buf, int buf_size, int keyframe);

        [StructLayout(LayoutKind.Sequential)]
        public struct AVBitStreamFilter
        {
            [MarshalAs(UnmanagedType.LPStr)]
            String name;

            [MarshalAs(UnmanagedType.I4)]
            int priv_data_size;

            [MarshalAs(UnmanagedType.FunctionPtr)]
            FilterCallback filter;

            IntPtr next; // AVBitStreamFilter *next
        };
    }
}
