PARAMETERS _rutaTabla
Application.Visible = .F.
*!*	WAIT WINDOW "íPreparando dbf &_tabla.!" AT 10,50 TIMEOUT 1
*!*	ALINES(arreglo, _rutaTabla, 0, '/')

ALINES(arreglo, _rutaTabla, 0, '\')
*!*	MESSAGEBOX(arreglo[4])

USE &_rutaTabla. EXCLUSIVE
IF arreglo[4] =="cliente"
	ALTER TABLE &_rutaTabla ALTER COLUMN motivo c(250)
ENDIF

IF FILE("&_rutaTabla..txt") 
DELETE FILE &_rutaTabla..txt

ENDIF 

COPY TO &_rutaTabla..txt delimiter  WITH CHARACTER TAB



*!*	WAIT WINDOW "íListo dbf &_tabla.!" AT 10,50 TIMEOUT 1
*!*	= MESSAGEBOX("Listo")
*!*	Application.Visible = .T.

*!*		RETURN .T.

*!*	_rutaTabla ="001\cliente"
*!*	ALINES(arreglo, _rutaTabla, 0, '\')
*!*	MESSAGEBOX(arreglo[2])