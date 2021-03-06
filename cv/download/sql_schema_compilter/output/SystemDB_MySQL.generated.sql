﻿-- generated by SQL COMPILER
-- time : 2018-07-12 09:57:59.240
-- do not modify
-- contact: icedac@gmail.com

USE bb_system;

SET FOREIGN_KEY_CHECKS = 0;

-- ****************************************************************************
-- *
-- * BEGIN RAWCODE
-- */


-- ****************************************************************************
-- *
-- *	table: static_acct_info
-- */
DROP TABLE IF EXISTS `static_acct_info`;
CREATE TABLE `static_acct_info`(
	`acct_level` int UNSIGNED NOT NULL
,	KEY (`acct_level`)
,	`max_ap` int UNSIGNED NOT NULL
,	`exp_to_lvup` int UNSIGNED NOT NULL
,	`total_exp_to_lvup` int UNSIGNED NOT NULL
,	PRIMARY KEY (`acct_level`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- [ERROR] failed to import from [../DB_data/static_acct_info.csv]


-- ****************************************************************************
-- *
-- *	table: static_char_info
-- */
DROP TABLE IF EXISTS `static_char_info`;
CREATE TABLE `static_char_info`(
	`char_index` int UNSIGNED NOT NULL
,	KEY (`char_index`)
,	`char_level` int UNSIGNED NOT NULL
,	KEY (`char_level`)
,	`exp_to_lvup` int UNSIGNED NOT NULL
,	`total_exp_to_lvup` int UNSIGNED NOT NULL
,	`char_att` int UNSIGNED NOT NULL
,	`char_def` int UNSIGNED NOT NULL
,	`char_health` int UNSIGNED NOT NULL
,	`char_ai` int UNSIGNED NOT NULL
,	`char_attr_train_time_0` int UNSIGNED NOT NULL COMMENT 'att,def,health,ai'
,	`char_attr_train_time_1` int UNSIGNED NOT NULL COMMENT 'att,def,health,ai'
,	`char_attr_train_time_2` int UNSIGNED NOT NULL COMMENT 'att,def,health,ai'
,	`char_attr_train_time_3` int UNSIGNED NOT NULL COMMENT 'att,def,health,ai'
,	`max_attr_point` int UNSIGNED NOT NULL COMMENT 'max attr points'
,	`attr_train_gold_cost` int UNSIGNED NOT NULL
,	`max_spec_point` int UNSIGNED NOT NULL
,	PRIMARY KEY (`char_index`, `char_level`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- [ERROR] failed to import from [../DB_data/static_char_info.csv]


-- ****************************************************************************
-- *
-- *	table: static_monster
-- */
DROP TABLE IF EXISTS `static_monster`;
CREATE TABLE `static_monster`(
	`monster_index` int UNSIGNED NOT NULL
,	KEY (`monster_index`)
,	`monster_code` varchar(50) NOT NULL
,	KEY (`monster_code`)
,	`monster_name` varchar(50) NOT NULL
,	`reward_code` varchar(50) NOT NULL
,	`monster_grade` int UNSIGNED NOT NULL DEFAULT '0' COMMENT '몬스터 등급'
,	`monster_gold_min` int UNSIGNED NOT NULL
,	`monster_gold_max` int UNSIGNED NOT NULL
,	`monster_exp` int UNSIGNED NOT NULL
,	`Monster_Lv` int UNSIGNED NOT NULL
,	`Attack_Type` varchar(50) NOT NULL
,	`Item_MinDam` int UNSIGNED NOT NULL
,	`Item_MaxDam` int UNSIGNED NOT NULL
,	`Item_Att` int UNSIGNED NOT NULL
,	`Item_Def` int UNSIGNED NOT NULL
,	`Item_Health` int UNSIGNED NOT NULL
,	`Dam_Slash` float NOT NULL
,	`Dam_Crash` float NOT NULL
,	`Dam_Pierce` float NOT NULL
,	`Item_Pen` int UNSIGNED NOT NULL
,	`Item_PenPer` float NOT NULL
,	`Item_CriPer` float NOT NULL
,	`Item_CriRat` float NOT NULL
,	`Item_TrueDam` int UNSIGNED NOT NULL
,	`TagRule` varchar(50) NOT NULL
,	PRIMARY KEY (`monster_index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- [ERROR] failed to import from [../DB_data/static_monster.csv]


-- ****************************************************************************
-- *
-- *	table: static_mission
-- */
DROP TABLE IF EXISTS `static_mission`;
CREATE TABLE `static_mission`(
	`mission_index` int UNSIGNED NOT NULL
,	KEY (`mission_index`)
,	`mission_code` varchar(50) NOT NULL
,	KEY (`mission_code`)
,	`mission_name` varchar(50) NOT NULL
,	`ap_cost` int UNSIGNED NOT NULL COMMENT 'ap 소모'
,	`mission_group` int UNSIGNED NOT NULL COMMENT '미션 그룹'
,	KEY (`mission_group`)
,	`boss_monster_code` varchar(50) NOT NULL
,	`opt_monster_count` int UNSIGNED NOT NULL COMMENT '잡몹 숫자'
,	`opt_monster_code_0` varchar(50) NOT NULL DEFAULT '0' COMMENT '잡몹 코드'
,	`opt_monster_code_1` varchar(50) NOT NULL DEFAULT '0' COMMENT '잡몹 코드'
,	`opt_monster_code_2` varchar(50) NOT NULL DEFAULT '0' COMMENT '잡몹 코드'
,	`opt_monster_code_3` varchar(50) NOT NULL DEFAULT '0' COMMENT '잡몹 코드'
,	`opt_monster_code_4` varchar(50) NOT NULL DEFAULT '0' COMMENT '잡몹 코드'
,	`opt_monster_code_5` varchar(50) NOT NULL DEFAULT '0' COMMENT '잡몹 코드'
,	`opt_monster_code_6` varchar(50) NOT NULL DEFAULT '0' COMMENT '잡몹 코드'
,	`opt_monster_code_7` varchar(50) NOT NULL DEFAULT '0' COMMENT '잡몹 코드'
,	`opt_monster_code_8` varchar(50) NOT NULL DEFAULT '0' COMMENT '잡몹 코드'
,	`opt_monster_code_9` varchar(50) NOT NULL DEFAULT '0' COMMENT '잡몹 코드'
,	`reward_code_if_cleard_1st` varchar(50) NOT NULL
,	`drop_item_index_0` int UNSIGNED NOT NULL DEFAULT '0' COMMENT '추가 데이터: 드랍 아이템 Index 리스트'
,	`drop_item_index_1` int UNSIGNED NOT NULL DEFAULT '0' COMMENT '추가 데이터: 드랍 아이템 Index 리스트'
,	`drop_item_index_2` int UNSIGNED NOT NULL DEFAULT '0' COMMENT '추가 데이터: 드랍 아이템 Index 리스트'
,	`drop_item_index_3` int UNSIGNED NOT NULL DEFAULT '0' COMMENT '추가 데이터: 드랍 아이템 Index 리스트'
,	`drop_item_index_4` int UNSIGNED NOT NULL DEFAULT '0' COMMENT '추가 데이터: 드랍 아이템 Index 리스트'
,	PRIMARY KEY (`mission_index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- [ERROR] failed to import from [../DB_data/static_mission.csv]


-- ****************************************************************************
-- *
-- *	table: static_item
-- */
DROP TABLE IF EXISTS `static_item`;
CREATE TABLE `static_item`(
	`item_index` int UNSIGNED NOT NULL
,	KEY (`item_index`)
,	`code` varchar(50) NOT NULL COMMENT '아이템 코드'
,	KEY (`code`)
,	`name` varchar(50) NOT NULL COMMENT '아이템 이름'
,	`item_tier` int UNSIGNED NOT NULL DEFAULT '0' COMMENT '아이템 아레나 티어'
,	`grade` int UNSIGNED NOT NULL DEFAULT '0' COMMENT '아이템 등급. 1:Normal 2:Uncommon ... 5: legend'
,	`max_stack` int UNSIGNED NOT NULL DEFAULT '1' COMMENT '아이템 최대 스택 수'
,	`max_level` int UNSIGNED NOT NULL DEFAULT '10' COMMENT '최대 레벨'
,	`item_desc` varchar(50) NOT NULL DEFAULT 'N/A' COMMENT '아이템 설명'
,	`slot` int UNSIGNED NOT NULL DEFAULT '255' COMMENT '아이템 장착 부위 0:skin ... 1:weapon, 88: skill_dummy 99: skill, 255:other'
,	`evolvable_item_index` int UNSIGNED NOT NULL DEFAULT '0' COMMENT ' 진화 가능하면 진화 가능한 아이템 Index, 0이면 불가능'
,	`equippable_char_index` int UNSIGNED NOT NULL DEFAULT '0' COMMENT ' 장착 가능한 캐릭터 인덱스, 0이면 아무나 '
,	`base_sell_price` int UNSIGNED NOT NULL DEFAULT '0'
,	`reward_code_on_use` varchar(50) NOT NULL DEFAULT '0'
,	`expire_in_hours` int UNSIGNED NOT NULL DEFAULT '0' COMMENT '0; 무제한, 주의: stackable 아이템에 기한제한을 넣으면 좇된다'
,	PRIMARY KEY (`item_index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- [ERROR] failed to import from [../DB_data/static_item.csv]


-- ****************************************************************************
-- *
-- *	table: static_reward
-- */
DROP TABLE IF EXISTS `static_reward`;
CREATE TABLE `static_reward`(
	`reward_index` int UNSIGNED NOT NULL AUTO_INCREMENT
,	KEY (`reward_index`)
,	`reward_code` varchar(50) NOT NULL
,	KEY (`reward_code`)
,	`expr_gold` varchar(50) NOT NULL DEFAULT '0'
,	`expr_ruby` varchar(50) NOT NULL DEFAULT '0'
,	`expr_honor` varchar(50) NOT NULL DEFAULT '0'
,	`expr_exp` varchar(50) NOT NULL DEFAULT '0'
,	`expr_ap` varchar(50) NOT NULL DEFAULT '0'
,	`expr_item_0` varchar(50) NOT NULL DEFAULT '0'
,	`expr_item_1` varchar(50) NOT NULL DEFAULT '0'
,	`expr_item_2` varchar(50) NOT NULL DEFAULT '0'
,	`expr_item_3` varchar(50) NOT NULL DEFAULT '0'
,	`expr_item_4` varchar(50) NOT NULL DEFAULT '0'
,	`expr_item_5` varchar(50) NOT NULL DEFAULT '0'
,	`expr_item_6` varchar(50) NOT NULL DEFAULT '0'
,	`expr_item_7` varchar(50) NOT NULL DEFAULT '0'
,	`expr_item_8` varchar(50) NOT NULL DEFAULT '0'
,	`expr_item_9` varchar(50) NOT NULL DEFAULT '0'
,	`expr_item_10` varchar(50) NOT NULL DEFAULT '0'
,	`expr_item_11` varchar(50) NOT NULL DEFAULT '0'
,	`expr_item_12` varchar(50) NOT NULL DEFAULT '0'
,	`expr_item_13` varchar(50) NOT NULL DEFAULT '0'
,	`expr_item_14` varchar(50) NOT NULL DEFAULT '0'
,	`expr_item_15` varchar(50) NOT NULL DEFAULT '0'
,	`expr_item_16` varchar(50) NOT NULL DEFAULT '0'
,	`expr_item_17` varchar(50) NOT NULL DEFAULT '0'
,	`expr_item_18` varchar(50) NOT NULL DEFAULT '0'
,	`expr_item_19` varchar(50) NOT NULL DEFAULT '0'
,	PRIMARY KEY (`reward_index`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 AUTO_INCREMENT = 1;

-- [ERROR] failed to import from [../DB_data/static_reward.csv]


-- ****************************************************************************
-- *
-- *	stored procedure: sp_import_char_info
-- */
DROP PROCEDURE IF EXISTS `sp_import_char_info`;
DELIMITER //
CREATE PROCEDURE `sp_import_char_info`(
	IN `start_char_index` int UNSIGNED
,	IN `start_char_level` int UNSIGNED
,	OUT `result` int UNSIGNED
)
fin:BEGIN
	
			select *
			from static_char_info as T
				where T.char_index > start_char_index or ( T.char_index = start_char_index and T.char_level >= start_char_level )
				order by char_index ASC, char_level ASC
				limit 32;
		
END//
DELIMITER ;


-- ****************************************************************************
-- *
-- *	stored procedure: sp_import_monster
-- */
DROP PROCEDURE IF EXISTS `sp_import_monster`;
DELIMITER //
CREATE PROCEDURE `sp_import_monster`(
	IN `start_monster_index` int UNSIGNED
,	OUT `result` int UNSIGNED
)
fin:BEGIN
	
			select *
			from static_monster as T
				where T.monster_index >= start_monster_index
				order by monster_index ASC
				limit 32;
		
END//
DELIMITER ;


-- ****************************************************************************
-- *
-- *	stored procedure: sp_import_mission
-- */
DROP PROCEDURE IF EXISTS `sp_import_mission`;
DELIMITER //
CREATE PROCEDURE `sp_import_mission`(
	IN `start_mission_index` int UNSIGNED
,	OUT `result` int UNSIGNED
)
fin:BEGIN
	
			select *
			from static_mission as T
				where T.mission_index >= start_mission_index
				order by mission_index ASC
				limit 32;
		
END//
DELIMITER ;


-- ****************************************************************************
-- *
-- *	stored procedure: sp_import_item
-- */
DROP PROCEDURE IF EXISTS `sp_import_item`;
DELIMITER //
CREATE PROCEDURE `sp_import_item`(
	IN `start_item_index` int UNSIGNED
,	OUT `result` int UNSIGNED
)
fin:BEGIN
	
			select *
			from static_item as T
				where T.item_index >= start_item_index
				order by item_index ASC
				limit 32;
		
END//
DELIMITER ;


-- ****************************************************************************
-- *
-- *	stored procedure: sp_import_reward
-- */
DROP PROCEDURE IF EXISTS `sp_import_reward`;
DELIMITER //
CREATE PROCEDURE `sp_import_reward`(
	IN `start_reward_index` int UNSIGNED
,	OUT `result` int UNSIGNED
)
fin:BEGIN
	
			select *
			from static_reward as T
				where T.reward_index >= start_reward_index
				order by reward_index ASC
				limit 32;
		
END//
DELIMITER ;


-- version check sp
DROP PROCEDURE IF EXISTS `sp_version_check`;
DELIMITER //
CREATE PROCEDURE `sp_version_check`( OUT version BIGINT, OUT minor_version BIGINT )
BEGIN
	set version = 32; -- 0x0000000000000020
	set minor_version = 131758306792050000; -- 0x01d4197b608c0d50
END//
DELIMITER ;

