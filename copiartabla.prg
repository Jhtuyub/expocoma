PARAMETERS _rutaTabla
Application.Visible = .F.
*!*	WAIT WINDOW "�Preparando dbf &_tabla.!" AT 10,50 TIMEOUT 1
*!*	ALINES(arreglo, _rutaTabla, 0, '/')

*!*	_rutaTabla = "EXPOCOMA\bin\Debug\tmp_expo\a\jtuyub\004\cliente"

ALINES(arreglo, _rutaTabla, 0, '\')
*!*	MESSAGEBOX(ALEN(arreglo))
*!*	MESSAGEBOX(arreglo[ALEN(arreglo)])
TRY
  FREE TABLE &_rutaTabla.
CATCH
*!*	  =MessageBox("No se puede ejecutar la aplicaci�n ahora, int�ntalo m�s tarde")
*!*	  QUIT
ENDTRY
*!*	IF arreglo[ALEN(arreglo)] =="inventa" OR arreglo[ALEN(arreglo)] == "inv001"
*!*		FREE TABLE &_rutaTabla.
*!*	ENDIF

USE &_rutaTabla. EXCLUSIVE
IF arreglo[ALEN(arreglo)] =="cliente"
	ALTER TABLE &_rutaTabla ALTER COLUMN motivo c(250)
ENDIF

IF FILE("&_rutaTabla..txt") 
DELETE FILE &_rutaTabla..txt

ENDIF 

COPY TO &_rutaTabla..txt delimiter  WITH CHARACTER TAB
CLOSE all


*!*	WAIT WINDOW "�Listo dbf &_tabla.!" AT 10,50 TIMEOUT 1
*!*	= MESSAGEBOX("Listo")
*!*	Application.Visible = .T.

*!*		RETURN .T.

*!*	_rutaTabla ="001\cliente"
*!*	ALINES(arreglo, _rutaTabla, 0, '\')
*!*	MESSAGEBOX(arreglo[2])