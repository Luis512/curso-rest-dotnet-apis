# Tarea

1. Investigar sobre el significado y concepto de CQRS en ASP.NET Core
    Command and Query Responsibility Segregation(CQRS) es un patron utilizado para separar o segregar las operaciones de lectura o escritura en base de datos de una aplicacion. Los operaciones de lectura son llamados Queries mientras que los llamados de escritura son llamdos Commands.

    Beneficios de utilizar CQRS
    -   Modularidad de codigo, lo que permite clases y modelos mas simples.
    -   Mayor escalabilidad dado que se puede separar los llamados en distintos microservicios.
    -   No existe gran dependencia en modelos unicos dado que se pueden crean n cantidad. 

    Desventajas de utilizar CQRS
    - En sistemas grandes y/o complejos, existe interdependecia(por ende agrega complejidad) entre llamados, por lo que muchas veces un llamado de lectura necesite hacer una escritura o viceversa.
