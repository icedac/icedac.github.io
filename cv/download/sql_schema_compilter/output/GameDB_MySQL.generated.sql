﻿-- generated by SQL COMPILER
-- time : 2018-07-12 09:57:58.981
-- do not modify
-- contact: icedac@gmail.com

USE bb_game;

SET FOREIGN_KEY_CHECKS = 0;

-- ****************************************************************************
-- *
-- * BEGIN RAWCODE
-- */

DROP EVENT IF EXISTS `ev_on_daily_rank`;
CREATE EVENT `ev_on_daily_rank`
	ON SCHEDULE
		EVERY 10 MINUTE STARTS '2000-01-01 05:00:00'
	ON COMPLETION PRESERVE
	ENABLE
	COMMENT ''
	DO call sp_balance_arena_rank(@r);	
	

-- ****************************************************************************
-- *
-- *	table: tbl_account
-- */
DROP TABLE IF EXISTS `tbl_account`;
CREATE TABLE `tbl_account`(
	`acct_id` bigint UNSIGNED NOT NULL AUTO_INCREMENT COMMENT '계정 Unique no'
,	`acct_id_external` bigint UNSIGNED NOT NULL
,	KEY (`acct_id_external`)
,	`acct_name` varchar(50) NOT NULL
,	KEY (`acct_name`)
,	`nickname` varchar(50) NOT NULL
,	`creation_time` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
,	PRIMARY KEY (`acct_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 AUTO_INCREMENT = 10000;

		-- you can add any code here after 'create table'
		

-- ****************************************************************************
-- *
-- *	table: tbl_account_game
-- */
DROP TABLE IF EXISTS `tbl_account_game`;
CREATE TABLE `tbl_account_game`(
	`acct_id` bigint UNSIGNED NOT NULL
,	KEY `FK__tbl_account_game_acct_id` (`acct_id`)
,	CONSTRAINT `FK__tbl_account_game_acct_id` FOREIGN KEY (`acct_id`) REFERENCES `tbl_account` (`acct_id`)
,	`acct_level` int UNSIGNED NOT NULL DEFAULT '1'
,	`acct_exp` int UNSIGNED NOT NULL DEFAULT '0'
,	`curr_char_id` bigint UNSIGNED NOT NULL DEFAULT '0'
,	`gold` int UNSIGNED NOT NULL
,	`ruby` int UNSIGNED NOT NULL
,	`max_ap` int UNSIGNED NOT NULL DEFAULT '71' COMMENT '최대 행동력'
,	`time_to_max_ap` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT 'ap가 최대치가 될 시간'
,	`ap_cool_time` int UNSIGNED NOT NULL DEFAULT '360' COMMENT 'ap 쿨타임(1차는데 걸리는 초)'
,	`last_logged_in` timestamp
,	`max_inventory_count` int UNSIGNED NOT NULL DEFAULT '500'
,	`last_processed_system_mail_id` int UNSIGNED NOT NULL DEFAULT '0'
,	`time_to_free_product_0` timestamp DEFAULT '0'
,	`time_to_free_product_1` timestamp DEFAULT '0'
,	`time_to_free_product_2` timestamp DEFAULT '0'
,	`time_to_free_product_3` timestamp DEFAULT '0'
,	`time_to_free_product_4` timestamp DEFAULT '0'
,	`time_to_free_product_5` timestamp DEFAULT '0'
,	`time_to_free_product_6` timestamp DEFAULT '0'
,	`time_to_free_product_7` timestamp DEFAULT '0'
,	`time_to_free_product_8` timestamp DEFAULT '0'
,	`time_to_free_product_9` timestamp DEFAULT '0'
,	`max_box_inventory_count` int UNSIGNED NOT NULL DEFAULT '4'
,	PRIMARY KEY (`acct_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;


-- ****************************************************************************
-- *
-- *	table: tbl_character
-- */
DROP TABLE IF EXISTS `tbl_character`;
CREATE TABLE `tbl_character`(
	`acct_id` bigint UNSIGNED NOT NULL
,	KEY `FK__tbl_character_acct_id` (`acct_id`)
,	CONSTRAINT `FK__tbl_character_acct_id` FOREIGN KEY (`acct_id`) REFERENCES `tbl_account` (`acct_id`)
,	`char_id` bigint UNSIGNED NOT NULL AUTO_INCREMENT
,	KEY (`char_id`)
,	`char_index` int UNSIGNED NOT NULL DEFAULT '0'
,	`char_level` int UNSIGNED NOT NULL DEFAULT '1'
,	`char_exp` int UNSIGNED NOT NULL DEFAULT '0'
,	`char_attr_pt_0` int UNSIGNED NOT NULL DEFAULT '1' COMMENT '0:att,def,health,ai'
,	`char_attr_pt_1` int UNSIGNED NOT NULL DEFAULT '1' COMMENT '0:att,def,health,ai'
,	`char_attr_pt_2` int UNSIGNED NOT NULL DEFAULT '1' COMMENT '0:att,def,health,ai'
,	`char_attr_pt_3` int UNSIGNED NOT NULL DEFAULT '1' COMMENT '0:att,def,health,ai'
,	`char_spec` varchar(20) NOT NULL COMMENT 'sepc string: 010021304201102'
,	`slot_item_id_0` bigint UNSIGNED NOT NULL DEFAULT '0' COMMENT '0: char_skin 1:weapon'
,	`slot_item_id_1` bigint UNSIGNED NOT NULL DEFAULT '0' COMMENT '0: char_skin 1:weapon'
,	`slot_item_id_2` bigint UNSIGNED NOT NULL DEFAULT '0' COMMENT '0: char_skin 1:weapon'
,	`slot_item_id_3` bigint UNSIGNED NOT NULL DEFAULT '0' COMMENT '0: char_skin 1:weapon'
,	`slot_item_id_4` bigint UNSIGNED NOT NULL DEFAULT '0' COMMENT '0: char_skin 1:weapon'
,	`slot_item_id_5` bigint UNSIGNED NOT NULL DEFAULT '0' COMMENT '0: char_skin 1:weapon'
,	`slot_item_id_6` bigint UNSIGNED NOT NULL DEFAULT '0' COMMENT '0: char_skin 1:weapon'
,	`slot_item_id_7` bigint UNSIGNED NOT NULL DEFAULT '0' COMMENT '0: char_skin 1:weapon'
,	`slot_item_id_8` bigint UNSIGNED NOT NULL DEFAULT '0' COMMENT '0: char_skin 1:weapon'
,	`slot_item_id_9` bigint UNSIGNED NOT NULL DEFAULT '0' COMMENT '0: char_skin 1:weapon'
,	`slot_item_skill_id_0` bigint UNSIGNED NOT NULL DEFAULT '0'
,	`slot_item_skill_id_1` bigint UNSIGNED NOT NULL DEFAULT '0'
,	`slot_item_skill_id_2` bigint UNSIGNED NOT NULL DEFAULT '0'
,	`slot_item_skill_id_3` bigint UNSIGNED NOT NULL DEFAULT '0'
,	`slot_item_skill_id_4` bigint UNSIGNED NOT NULL DEFAULT '0'
,	`slot_item_skill_id_5` bigint UNSIGNED NOT NULL DEFAULT '0'
,	`slot_item_skill_id_6` bigint UNSIGNED NOT NULL DEFAULT '0'
,	`slot_item_skill_id_7` bigint UNSIGNED NOT NULL DEFAULT '0'
,	`slot_item_skill_id_8` bigint UNSIGNED NOT NULL DEFAULT '0'
,	`slot_item_skill_id_9` bigint UNSIGNED NOT NULL DEFAULT '0'
,	PRIMARY KEY (`char_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 AUTO_INCREMENT = 20000;


-- ****************************************************************************
-- *
-- *	table: tbl_log_db
-- */
DROP TABLE IF EXISTS `tbl_log_db`;
CREATE TABLE `tbl_log_db`(
	`log_id` bigint UNSIGNED NOT NULL AUTO_INCREMENT
,	`log_time` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
,	`msg` varchar(512) NOT NULL
,	PRIMARY KEY (`log_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 AUTO_INCREMENT = 10000;


-- ****************************************************************************
-- *
-- *	stored procedure: sp_get_account_box
-- */
DROP PROCEDURE IF EXISTS `sp_get_account_box`;
DELIMITER //
CREATE PROCEDURE `sp_get_account_box`(
	IN `acct_id` bigint UNSIGNED
,	OUT `result` int UNSIGNED
)
fin:BEGIN
	
	CALL fn_check_account( acct_id, result ); IF ( result != 0 ) THEN SELECT 0 LIMIT 0; LEAVE fin; END IF;
	-- CALL fn_check_character( char_id, result ); IF ( result != 0 ) THEN SELECT 0 LIMIT 0; LEAVE fin; END IF;

	SELECT AB.*
		FROM tbl_account_box AS AB
		WHERE AB.acct_id = acct_id;

	SET result = 0;
		
END//
DELIMITER ;


-- ****************************************************************************
-- *
-- *	stored procedure: sp_account_login
-- */
DROP PROCEDURE IF EXISTS `sp_account_login`;
DELIMITER //
CREATE PROCEDURE `sp_account_login`(
	IN `acct_name` varchar(50)
,	IN `ip_address` varchar(50)
,	IN `port` int UNSIGNED
,	OUT `result` int UNSIGNED
)
fin:BEGIN
	
	CALL fn_check_account( acct_id, result ); IF ( result != 0 ) THEN LEAVE fin; END IF;
	-- CALL fn_check_character( char_id, result ); IF ( result != 0 ) THEN LEAVE fin; END IF;
	
	UPDATE tbl_account_game
		SET last_logged_in = NOW()
		WHERE tbl_account_game.acct_id = @acct_id;

	INSERT INTO `tbl_log_account_login`(
		`acct_id`, `acct_name`, `ip_address`, `port` )
		VALUES (
			@acct_id, acct_name, ip_address, port
		);

	SET result = 0;
		
END//
DELIMITER ;


-- ****************************************************************************
-- *
-- *	stored procedure: sp_new_item_list
-- */
DROP PROCEDURE IF EXISTS `sp_new_item_list`;
DELIMITER //
CREATE PROCEDURE `sp_new_item_list`(
	IN `acct_id` bigint UNSIGNED
,	IN `new_item_index_0` int UNSIGNED
,	IN `new_item_index_1` int UNSIGNED
,	IN `new_item_index_2` int UNSIGNED
,	IN `new_item_index_3` int UNSIGNED
,	IN `new_item_index_4` int UNSIGNED
,	IN `amount_0` int UNSIGNED
,	IN `amount_1` int UNSIGNED
,	IN `amount_2` int UNSIGNED
,	IN `amount_3` int UNSIGNED
,	IN `amount_4` int UNSIGNED
,	OUT `result` int UNSIGNED
,	OUT `added_count` int UNSIGNED
,	OUT `added_item_id_0` bigint UNSIGNED
,	OUT `added_item_id_1` bigint UNSIGNED
,	OUT `added_item_id_2` bigint UNSIGNED
,	OUT `added_item_id_3` bigint UNSIGNED
,	OUT `added_item_id_4` bigint UNSIGNED
)
fin:BEGIN
	
	CALL fn_check_account( acct_id, result ); IF ( result != 0 ) THEN LEAVE fin; END IF;
	-- CALL fn_check_character( char_id, result ); IF ( result != 0 ) THEN LEAVE fin; END IF;

	set added_count = 0;

	start transaction;

	if ( new_item_index_0 != 0 ) then
		call fn_new_item( acct_id, new_item_index_0, amount_0, result, added_item_id_0 );
		if ( result != 0 ) then rollback; leave fin; end if;
		set added_count = added_count + 1;
	end if;
	if ( new_item_index_1 != 0 ) then
		call fn_new_item( acct_id, new_item_index_1, amount_1, result, added_item_id_1 );
		if ( result != 0 ) then rollback; leave fin; end if;
		set added_count = added_count + 1;
	end if;
	if ( new_item_index_2 != 0 ) then
		call fn_new_item( acct_id, new_item_index_2, amount_2, result, added_item_id_2 );
		if ( result != 0 ) then rollback; leave fin; end if;
		set added_count = added_count + 1;
	end if;
	if ( new_item_index_3 != 0 ) then
		call fn_new_item( acct_id, new_item_index_3, amount_3, result, added_item_id_3 );
		if ( result != 0 ) then rollback; leave fin; end if;
		set added_count = added_count + 1;
	end if;
	if ( new_item_index_4 != 0 ) then
		call fn_new_item( acct_id, new_item_index_4, amount_4, result, added_item_id_4 );
		if ( result != 0 ) then rollback; leave fin; end if;
		set added_count = added_count + 1;
	end if;

	commit;

	SET result = 0;
		
END//
DELIMITER ;


-- ****************************************************************************
-- *
-- *	stored procedure: fn_new_item
-- */
DROP PROCEDURE IF EXISTS `fn_new_item`;
DELIMITER //
CREATE PROCEDURE `fn_new_item`(
	IN `in_acct_id` bigint UNSIGNED
,	IN `new_item_index` int UNSIGNED
,	IN `amount` int UNSIGNED
,	OUT `result` int UNSIGNED
,	OUT `added_item_id` bigint UNSIGNED
)
fin:BEGIN
	
	CALL fn_check_account( acct_id, result ); IF ( result != 0 ) THEN LEAVE fin; END IF;
	-- CALL fn_check_character( char_id, result ); IF ( result != 0 ) THEN LEAVE fin; END IF;

	set added_item_id = 0;
	set @found = 0;

	-- 존재하는 아이템인지 확인과 max_stack 얻어오기
	set @max_stack = 0;
	select max_stack into @max_stack
		from bb_system.static_item as SI
		where SI.item_index = new_item_index;
	if ( @max_stack = 0 ) then
		set result = 301; -- ERR_RESOURCE_NOT_FOUND(301)
		leave fin;
	end if;

	-- stack안되는데 amount가 1이 아니면 에러
	if ( @max_stack = 1 and amount != 1 ) then
		set result = 401; -- ERR_ACCESS_DENIED_PROTECTED(401)
		leave fin;
	end if;

	-- 같은 인덱스의 아이템이 있는지 확인
	set @exist_item_id = 0;
	select item_id, amount into @exist_item_id, @exist_amount
	 	from tbl_inventory as I 
	 	where I.acct_id = in_acct_id and I.item_index = new_item_index
	 	order by item_id DESC
	 	limit 1;

	-- Stack 되는 아이템이면 존재하는 아이템에 수량 추가
	if ( @exist_item_id != 0 and @max_stack > 1 ) then
		set @amount = @exist_amount + amount;
		-- if ( @amount > @max_stack ) then set @amount = @max_stack; end if;
		update tbl_inventory AS I
			set I.amount = I.amount + amount
			where I.item_id = @exist_item_id;
		set result = 0;
		set added_item_id = @exist_item_id;
		leave fin;
	end if;

	-- 일단 아이템이 없으면 아이템 추가 한다.
	insert into `tbl_inventory` (`acct_id`, `item_index`, `amount` ) values ( in_acct_id, new_item_index, amount );
	
	set added_item_id = LAST_INSERT_ID();

	if ( added_item_id = 0 ) then
		set result = 3; -- ACCESS_DENIED
		set added_item_id = 0;
		leave fin;
	end if;

	set result = 0; -- 성공
		
END//
DELIMITER ;


-- version check sp
DROP PROCEDURE IF EXISTS `sp_version_check`;
DELIMITER //
CREATE PROCEDURE `sp_version_check`( OUT version BIGINT, OUT minor_version BIGINT )
BEGIN
	set version = 32; -- 0x0000000000000020
	set minor_version = 131758306789640000; -- 0x01d4197b60674740
END//
DELIMITER ;

