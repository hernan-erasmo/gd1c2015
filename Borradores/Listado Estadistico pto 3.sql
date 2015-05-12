/*
Listado Estadistico

3- Clientes con mayor cantidad de transacciones realizadas entre cuentas propias


Creo que esta consulta cumple con lo que necesitamos. Les mando al mail la imagen "Consulta Estadisticas 3.jpg" que tiene solo la parte del DER que
se usa en la consulta para que sea un poco más facil de entender.
No creo que sea necesario crear una tabla para cada estadistica, capaz en algun caso sea necesario pero no creo que sea en todos.
*/

SELECT top 5 c.cl_id, count(*)
FROM Transferencia t right join Cliente c on (c.cl_id = t.tr_cliente)
		right join Cuenta ct on (t.tr_ctaDestino = ct.cta_id)
		right join Cliente c2 on (ct.cta_cliente = c2.cl_id)
where c.cl_id = c2.cl_id
group by c.cl_id
order by count(*) desc



----------------------------------------------------------------------
/*
Cliente
	cl_id
	
Cuenta
	cta_id
	cta_cliente
	
Transferencia
	tr_cliente
	tr_ctaOrigen
	tr_ctaDestino

*/	
