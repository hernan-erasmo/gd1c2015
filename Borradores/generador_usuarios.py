# coding: utf-8

import string
import random
import hashlib

entrada = ''
username_len = 8
password_len = 6
getdate = 'GETDATE()'
pregunta = '\'¿Cuál es el nombre de este grupo?\''
respuesta = 'SARASA'
estado = '1'

for x in range(1,166):
	entrada += '--' + str(x) + '\r\n'
	
	#username
	entrada += '(\t' + '\'' + ''.join(random.choice(string.ascii_lowercase) for i in range(username_len)) + '\'' + ',\r\n'

	#password
	passwd = ''.join(random.choice(string.ascii_lowercase + string.digits) for i in range(password_len))
	entrada += '\t' + '\'' + hashlib.sha256(passwd).hexdigest() + '\'' + ',\t--' + passwd + '\r\n'

	#Fecha creacion
	entrada += '\t' + getdate + ',\r\n'

	#Fecha modificacion
	entrada += '\t' + getdate + ',\r\n'

	#Pregunta
	entrada += '\t' + pregunta + ',\r\n'

	#Respuesta
	entrada += '\t' + '\'' + hashlib.sha256(respuesta).hexdigest() + '\'' + ',\t--' + respuesta + '\r\n'

	#estado
	entrada += '\t' + estado + ',\t--Habilitado\r\n'

	entrada += '\tNULL),' + '\r\n'

	print entrada