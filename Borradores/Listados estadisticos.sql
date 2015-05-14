/*
  Estas son las querys que se me ocurrieron. 
  Para las cuentas inhabilitadas (listado 1) es necesario una tabla extra.
  No me di cuenta que se filtraba por fecha...
*/

/*
	Listado estadistico 5:
		"Total facturado para los distintos tipos de cuentas"
		
	Yo entiendo que se refiere a lo cobrado por eso uso la fecha de la factura que se genera cuando el cliente paga.
	Pero si se refiere a la fecha en que se realizo la operación, se usa en el filtro la fecha de item_factura
	y ya no es neceasrio el join con Factura.
*/

SELECT TOP 5 tcta.descripcion, sum(itm.itemfact_importe)
FROM ItemFactura itm join right Factura f on (f.factura_id = itm.itemfact_factura_id)
				join right Cuenta cta on (itm.itemfact_cuenta_id = cta.cuenta_id)
				join right TipoCuenta tcta on (cta.cuenta_tipocta_id = tcta.tipocta_id)
WHERE (f.factura_fecha between @inicioTrimestre and @finTrimestre) and (@anioTrimestre = year(f.factura_fecha))
GROUP BY tcta.descripcion
ORDER BY sum(itm.itemfact_importe) DESC


/*
	Listado estadistico 4:
		"Países con mayor cantidad de movimientos tanto ingresos como egresos"

	Para mi:
	Deposito es un INGRESO al pais al que pertenece la cuenta
	Retiro es un EGRESO al pais al que pertenece la cuenta
	Transferencia es un INGRESO al pais al que pertenece la cuenta destino y EGRESO al pais de la cuenta origen
	
	O una tabla para grabar los movimientos por pais
	o hacer un query que saque esa info de las tablas ya creadas (las fechas estan)

	
*/

/*
	Listado estadistico 2:
		"Cliente con mayor cantidad de comisiones facturadas en todas sus cuentas"
		
		Aca aplica lo mismo que en el punto 5, si se refiere a la fecha de la factura o a la fecha de la operacion
*/
SELECT TOP 5 cli.cliente_id, count(*)
FROM ItemFactura itm join right Factura f on (f.factura_id = itm.itemfact_factura_id)
				join right Cuenta cta on (itm.itemfact_cuenta_id = cta.cuenta_id)
				join right Cliente cli on (cli.cliente_id = cta.cuenta_cliente_id)
WHERE (f.factura_fecha between @inicioTrimestre and @finTrimestre) and (@anioTrimestre = year(f.factura_fecha))
GROUP BY cli.cliente_id
ORDER BY count(*) DESC

/*
	Listado estadistico 1:
		"Clientes que alguna de sus cuentas fueron inhabilitadas por no pagar los costos de transacción"
		
		Aca necesitariamos una tabla, para registrar el cambio de estado de las distintas cuentas.

		Tabla LogCuentas
			cuenta_id
			estado_descripcion
			fecha
			
		Cualquier cambio de estado se graba en esa tabla
		
*/
SELECT distinct cli.cliente_id
FROM LogCuentas lc join right Cuenta cta on (lc.cuenta_id = cta.cuenta_id)
				join right Cliente cli (cli.cliente_id = cta.cuenta_cliente_id)
WHERE (lc.fecha between @inicioTrimestre and @finTrimestre) 
	and (@anioTrimestre = year(lc.fecha)) 
	and (lc.estado_descripcion = 'inhabilitado')

