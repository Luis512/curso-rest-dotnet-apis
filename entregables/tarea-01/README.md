<ol>
    <li><h4>En que consiste CORS?</h4>
            <p>CORS  (Cross Origin Resource Sharing) es una tecnoglogia orientada a la obtencion de recursos fuera del origen de quien lo requiera. Dicese de otra manera, solicitar algun (hojas de estilo, scripts, imagenes u otros) que se encuentre fuera del servidor de servicio/pagina que lo solicita.
                Para utilizar un ejemplo: la pagina web X almacenada en el server A solicita una imagen que se encuentra almacenada en el server B, a esto se le llama Cross Origin Resource Sharing. Con CORS es necesario especificar los servidores de donde y como se obtendra algun rescurso con el fin de minimizar riesgos de seguridad.
                El manejo de solicitudes CORS se hace mediante el uso de HTTP headers siguiendo el siguiente standard :Access-Control-Allow-Origin, Access-Control-Allow-Credentials,Access-Control-Allow-Headers, Access-Control-Allow-Methods, Access-Control-Expose-Headers, Access-Control-Max-Age, Access-Control-Request-Headers, Access-Control-Request-Method y Origin.</p>
    </li>
    <li><h4>Incluir un ejemplo de solucion en C#, JavaScript, Python, Go o algun otro lenguaje</h4>
        <p><h5>Server.js</h5>     
        
```js
var express = require('express'),
  app = express(),
  port = process.env.PORT || 3000;

var cors = require('cors');

var routes = require('./api/routes/routes'); 
routes(app);

app.use(cors());
app.listen(port);
```   
   </p>
    <p><h5>Controller.js</h5>     
        
```js
'use strict';

exports.getSomething = function(req, res) {  
    res.setHeader('Access-Control-Allow-Origin', '*');
    res.json({ message: 'Task successfully obtained.' }); 
};
```   
   </p>
    </li>
    <li><h4>Que es Docker y en que consiste el concepto de Imagen y Contenedor?</h4>
        <p>
                Docker es una platarforma que utiliza el concepto de contenedor para correr aplicaciones en ambiente aislados, estos ambientes son virtualizados siguiendo el concepto PaaS o Platform as a service, donde se enfoca mas el como correr una aplicacion sin invertir tiempo en configuracion no relacionadas a ella, como por ejemplo configuracion de OS. 
                Un docker contiene la aplicacion como un paquete, esto implica todo lo necesario para que dicha aplicacion funcione de la manera que se espera como codigo, librerias y recursos de diferente indole.<br/>             
                Los sistemas que utilizan el concepto de contenedor como lo permite Docker, presentan cierto beneficios bastante atractivos al momento de querer crear una aplicacion o API, como lo son:                
            </p>
        <ul>            
            <li>Son portables o de facil ejecucion en diferentes ambientes.</li>
            <li>Escalables.</li>
            <li>Sistemas sumamente modulares, dado que se puede separar funcionalidades en contenedores.</li>
            <li>Desarrollo mas agil tanto para ambiente de testing como produccion.</li>
        </ul>
        <br/> <p>
            En cuanto al concepto de imagen, podemos decir que se trata la base o plantilla donde se creara el contenedor, mientras que este ultimo es la instancia que se encuentra en ejecucion dentro de la imagen.
        </p>        
    </li>
</ol>
