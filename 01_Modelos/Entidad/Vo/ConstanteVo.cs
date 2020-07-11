using System;

namespace Entidad.Vo
{
    public static class ConstanteVo
    {
        public static string AccessKeyAws = "";
        public static string SecretAccessKeyAws = "";
        public static string UrlAmazon = "https://encuentralo.s3.us-east-2.amazonaws.com/";

        //public static string NombreDominio = "https://localhost:44314";
        public static string NombreDominio = "<h4><b>Indice de métodos: </b></h4>" +
            Environment.NewLine +
            " * http://api-find.control-zeta.net/swagger" +
            Environment.NewLine +
            " * https://api-find.control-zeta.net/swagger" +
            Environment.NewLine +
            Environment.NewLine +
            "<h4><b>Ejemplo de invocacion: </b></h4>" +
            Environment.NewLine +
            " * http://api-find.control-zeta.net/api/Usuario" +
            Environment.NewLine +
            " * https://api-find.control-zeta.net/api/Usuario" +
            Environment.NewLine +
            Environment.NewLine +
            "<h4><b>Los parametros fecha deberan ser enviados en string con el formato:</b></h4>" +
            Environment.NewLine +
            " * yyyy/MM/dd HH:mm:ss (Fecha y Hora 24)" +
            Environment.NewLine +
            " * yyyy/MM/dd (Solo Fecha)" +
            Environment.NewLine +
            Environment.NewLine +
            "Actualizado al 07/07/2020" +
            Environment.NewLine +
            Environment.NewLine +
            "<h4><b>Los modelos en Android se deben generar desde esta pagina:</b></h4>" +
            "http://www.jsonschema2pojo.org/" +
            Environment.NewLine +
            Environment.NewLine +
            "<h4><b>Retrofit TOKEN</b></h4>" +
            "<div style=\"background-color:white;\"><pre>import okhttp3.Interceptor;<br />" +
            "import okhttp3.Request;<br />" +
            "import okhttp3.Response;<br />" +
            "public class AuthInterceptor implements Interceptor<br />" +
            "{<br />" +
            "   @Override<br />" +
            "   public Response intercept(Chain chain) throws IOException<br />" +
            "   {<br />" +
            "       String token = SharedPreferencesManager.getStringValue(Constantes.PREF_TOKEN);<br />" +
            "       Request request = chain.request()<br />" +
            "                   .newBuilder()<br />" +
            "                   .addHeader(\"Authorization\", \"Bearer \" + token).build();<br />" +
            "       return chain.proceed(request);<br />" +
            "   }<br />" + 
            "}</pre></div>" + Environment.NewLine +
            Environment.NewLine +
            "<h4><b>Usar el Interceptor en la construccion de Retrofit</b></h4>" + Environment.NewLine +
            "<div style=\"background-color:white;\"><pre>public RetrofitClientAuth()" + Environment.NewLine +
            "{" + Environment.NewLine +
            "    //Incluir el token en la peticion" + Environment.NewLine +
            "    OkHttpClient.Builder okHttpClientBuilder = new OkHttpClient.Builder();" + Environment.NewLine +
            "    okHttpClientBuilder.addInterceptor(new AuthInterceptor());" + Environment.NewLine +
            "    OkHttpClient cliente = okHttpClientBuilder.build();" + Environment.NewLine +
            "    retrofit = new Retrofit.Builder()" + Environment.NewLine +
            "            .baseUrl(\"https://api-find.control-zeta.net/api/\")" + Environment.NewLine +
            "            .addConverterFactory(GsonConverterFactory.create())" + Environment.NewLine +
            "            .client(cliente)" + Environment.NewLine +
            "            .build();" + Environment.NewLine +
            "" + Environment.NewLine +
            "}</pre></div>" + Environment.NewLine +Environment.NewLine +
            "<h4><b>Enviar imágen multipart (Interfaz)</b></h4>" + Environment.NewLine +
            "<div style=\"background-color:white;\"><pre>@Multipart<br />" +
            "@POST(\"Usuario/ImagenMetodo3\")<br />" +
            "Call&ltUsuarioSubirImagenResponseDto&gt SubirImagen(@Part MultipartBody.Part archivo,<br />" +
            "                   @Part(\"IdUsuario\") RequestBody idUsuario);</pre></div>" + 
            Environment.NewLine+ Environment.NewLine +
            "<h4><b>Implementación</b></h4>" + 
            "<div style=\"background-color:white;\"><pre>public void SubirImagen(String rutaArchivo)<br />" +
            "{<br />" +
            "   File file = new File(rutaArchivo);<br />" +
            "   String idUsuarioAutenticado = Integer.toString(SharedPreferencesManager.getIntValue(Constantes.PREF_IDUSUARIOAUTENTICADO));<br />" +
            "   RequestBody photoContent = RequestBody.create(MediaType.parse(\"multipart/form-data\"), file);<br />" +
            "   RequestBody idUsuario = RequestBody.create(MediaType.parse(\"text/plain\"), idUsuarioAutenticado);<br />" +
            "   MultipartBody.Part photo = MultipartBody.Part.createFormData(\"Archivo\", file.getName(), photoContent);<br />" +
            "   Call&lt;UsuarioSubirImagenResponseDto&gt; llamada = usuarioService.SubirImagen(photo, idUsuario);<br />" +
            "   llamada.enqueue(new Callback&lt;UsuarioSubirImagenResponseDto&gt;() {<br />" +
            "   @Override<br />" +
            "   public void onResponse(Call&lt;UsuarioSubirImagenResponseDto&gt; call, Response&lt;UsuarioSubirImagenResponseDto&gt;response)<br />" +
            "   {<br />......</pre></div>" + Environment.NewLine;

    }
}
