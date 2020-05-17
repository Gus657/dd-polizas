CREATE TABLE `tipo_polizas` (
  `id_poliza` int(11) NOT NULL,
  `nombre` varchar(250) NOT NULL,
  `descripcion` varchar(250) NOT NULL,
  `estado` tinyint(1) DEFAULT '1'
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

ALTER TABLE `tipo_polizas`
  ADD PRIMARY KEY (`id_poliza`);

  ALTER TABLE `tipo_polizas`
  MODIFY `id_poliza` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

INSERT INTO `tipo_polizas` (`id_poliza`, `nombre`, `descripcion`, `estado`) VALUES
(1, 'VENTAS', 'VENTAS', 1);


CREATE TABLE `poliza_encabezados` (
  `id_poliza` int(11) NOT NULL,
  `fecha` date DEFAULT NULL,
  `tipo_poliza` varchar(45) DEFAULT NULL,
  `fecha_inicio` date DEFAULT NULL,
  `fecha_fin` date DEFAULT NULL,
  `estado` tinyint(1) DEFAULT '1'
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

ALTER TABLE `poliza_encabezados`
  ADD PRIMARY KEY (`id_poliza`),
  ADD KEY `tipo_poliza` (`tipo_poliza`);

CREATE TABLE `poliza_detalles` (
  `numero_movimiento` int(5) NOT NULL,
  `id_poliza` int(11) NOT NULL,
  `id_cuenta` varchar(250) NOT NULL,
  `debe` float DEFAULT '0',
  `haber` float DEFAULT '0',
  `estado` tinyint(1) DEFAULT '1'
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

ALTER TABLE `poliza_detalles`
  ADD PRIMARY KEY (`numero_movimiento`,`id_cuenta`) USING BTREE,
  ADD KEY `añlas,_idx` (`id_poliza`),
  ADD KEY `,ñl_idx` (`id_cuenta`);

ALTER TABLE `poliza_detalles`
  MODIFY `numero_movimiento` int(5) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=23;

ALTER TABLE `poliza_detalles`
  ADD CONSTRAINT `poliza_detalles_ibfk_1` FOREIGN KEY (`id_poliza`) REFERENCES `poliza_encabezados` (`id_poliza`) ON DELETE CASCADE ON UPDATE CASCADE;

