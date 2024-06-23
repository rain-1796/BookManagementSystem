/*
 Navicat Premium Data Transfer

 Source Server         : demo
 Source Server Type    : MySQL
 Source Server Version : 80036 (8.0.36)
 Source Host           : localhost:3306
 Source Schema         : bookdb

 Target Server Type    : MySQL
 Target Server Version : 80036 (8.0.36)
 File Encoding         : 65001

 Date: 23/06/2024 13:03:21
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for t_admin
-- ----------------------------
DROP TABLE IF EXISTS `t_admin`;
CREATE TABLE `t_admin`  (
  `id` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `psw` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of t_admin
-- ----------------------------
INSERT INTO `t_admin` VALUES ('001', '123');

-- ----------------------------
-- Table structure for t_book
-- ----------------------------
DROP TABLE IF EXISTS `t_book`;
CREATE TABLE `t_book`  (
  `id` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `name` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `kind_id` int NULL DEFAULT NULL,
  `author` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `press` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `number` int NULL DEFAULT NULL
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of t_book
-- ----------------------------
INSERT INTO `t_book` VALUES ('9787020162987', '三国演义', 4, '罗贯中', '人民文学出版社', 10);
INSERT INTO `t_book` VALUES ('9787302521426', '西游记', 4, '吴承恩', '清华大学出版社', 12);
INSERT INTO `t_book` VALUES ('9787559478207', '沙丘', 2, '弗兰克·赫伯特', '江苏凤凰文艺出版社', 20);
INSERT INTO `t_book` VALUES ('9787521752236', '史蒂夫·乔布斯传', 5, '沃尔特·艾萨克森', '中信出版社', 22);
INSERT INTO `t_book` VALUES ('9787559602152', '明朝那些事儿', 3, '当年明月', '北京联合出版社', 18);
INSERT INTO `t_book` VALUES ('9787508678498', '历史的温度', 3, '张玮', '中信出版社', 29);
INSERT INTO `t_book` VALUES ('9787532763696', '卡拉马佐夫兄弟', 1, '陀思妥耶夫斯基', '上海译文出版社', 27);
INSERT INTO `t_book` VALUES ('9787512502352', '毛泽东传', 5, '迪克·威尔逊', '国际文化出版社', 26);
INSERT INTO `t_book` VALUES ('9787536692930', '三体1：地球往事', 2, '刘慈欣', '重庆出版社', 24);
INSERT INTO `t_book` VALUES ('9787532776771', '挪威的森林', 1, '村上春树', '上海译文出版社', 14);
INSERT INTO `t_book` VALUES ('9787040591255', '数据库原理与应用', 2, '王珊', '长安大学出版社', 36);

-- ----------------------------
-- Table structure for t_kind
-- ----------------------------
DROP TABLE IF EXISTS `t_kind`;
CREATE TABLE `t_kind`  (
  `id` int NOT NULL AUTO_INCREMENT,
  `kind` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 6 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of t_kind
-- ----------------------------
INSERT INTO `t_kind` VALUES (1, '文学');
INSERT INTO `t_kind` VALUES (2, '科幻');
INSERT INTO `t_kind` VALUES (3, '历史');
INSERT INTO `t_kind` VALUES (4, '小说');
INSERT INTO `t_kind` VALUES (5, '传记');

-- ----------------------------
-- Table structure for t_lend
-- ----------------------------
DROP TABLE IF EXISTS `t_lend`;
CREATE TABLE `t_lend`  (
  `no` int NOT NULL AUTO_INCREMENT,
  `uid` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `bid` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `bname` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `datetime` datetime NULL DEFAULT NULL,
  `overtime` char(3) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  PRIMARY KEY (`no` DESC) USING BTREE,
  INDEX `user_id`(`uid` ASC) USING BTREE,
  INDEX `book_id`(`bid` ASC) USING BTREE,
  CONSTRAINT `user_id` FOREIGN KEY (`uid`) REFERENCES `t_user` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 12 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of t_lend
-- ----------------------------
INSERT INTO `t_lend` VALUES (10, '1004', '9787532763696', '卡拉马佐夫兄弟', '2024-05-29 21:57:43', '已超期');
INSERT INTO `t_lend` VALUES (9, '1003', '9787521752236', '史蒂夫·乔布斯传', '2024-05-29 15:44:25', '已超期');
INSERT INTO `t_lend` VALUES (8, '1003', '9787536692930', '三体1：地球往事', '2024-05-29 15:44:21', '已超期');
INSERT INTO `t_lend` VALUES (7, '1003', '9787512502352', '毛泽东传', '2024-05-28 10:55:35', '已超期');
INSERT INTO `t_lend` VALUES (6, '1002', '9787521752236', '史蒂夫·乔布斯传', '2024-05-27 13:41:12', '已超期');
INSERT INTO `t_lend` VALUES (5, '1002', '9787302521426', '西游记', '2024-05-26 16:57:26', '已超期');
INSERT INTO `t_lend` VALUES (4, '1002', '9787020162987', '三国演义', '2024-05-25 13:26:18', '已超期');
INSERT INTO `t_lend` VALUES (3, '1001', '9787508678498', '历史的温度', '2024-05-25 09:30:24', '已超期');
INSERT INTO `t_lend` VALUES (2, '1001', '9787532776771', '挪威的森林', '2024-05-20 12:15:48', '已超期');

-- ----------------------------
-- Table structure for t_lend_all
-- ----------------------------
DROP TABLE IF EXISTS `t_lend_all`;
CREATE TABLE `t_lend_all`  (
  `no` int NOT NULL AUTO_INCREMENT,
  `uid` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `bid` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `bname` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `datetime` datetime NULL DEFAULT NULL,
  `overtime` char(3) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  PRIMARY KEY (`no` DESC) USING BTREE,
  INDEX `user_id_all`(`uid` ASC) USING BTREE,
  INDEX `book_id_all`(`bid` ASC) USING BTREE,
  CONSTRAINT `user_id_all` FOREIGN KEY (`uid`) REFERENCES `t_user` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 12 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of t_lend_all
-- ----------------------------
INSERT INTO `t_lend_all` VALUES (11, '1005', '9787040591255', '数据库原理与应用', '2024-05-29 22:51:14', '已超期');
INSERT INTO `t_lend_all` VALUES (10, '1004', '9787532763696', '卡拉马佐夫兄弟', '2024-05-29 21:57:43', '已超期');
INSERT INTO `t_lend_all` VALUES (9, '1003', '9787521752236', '史蒂夫·乔布斯传', '2024-05-29 15:44:25', '已超期');
INSERT INTO `t_lend_all` VALUES (8, '1003', '9787536692930', '三体1：地球往事', '2024-05-29 15:44:21', '已超期');
INSERT INTO `t_lend_all` VALUES (7, '1003', '9787512502352', '毛泽东传', '2024-05-28 10:55:35', '已超期');
INSERT INTO `t_lend_all` VALUES (6, '1002', '9787521752236', '史蒂夫·乔布斯传', '2024-05-27 13:41:12', '已超期');
INSERT INTO `t_lend_all` VALUES (5, '1002', '9787302521426', '西游记', '2024-05-26 16:57:26', '已超期');
INSERT INTO `t_lend_all` VALUES (4, '1002', '9787020162987', '三国演义', '2024-05-25 13:26:18', '已超期');
INSERT INTO `t_lend_all` VALUES (3, '1001', '9787508678498', '历史的温度', '2024-05-25 09:30:24', '已超期');
INSERT INTO `t_lend_all` VALUES (2, '1001', '9787532776771', '挪威的森林', '2024-05-20 12:15:48', '已超期');
INSERT INTO `t_lend_all` VALUES (1, '1001', '9787559478207', '沙丘', '2024-05-18 17:28:13', '已超期');

-- ----------------------------
-- Table structure for t_user
-- ----------------------------
DROP TABLE IF EXISTS `t_user`;
CREATE TABLE `t_user`  (
  `id` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `name` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `sex` char(2) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `psw` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE,
  CONSTRAINT `sex` CHECK ((`sex` = _utf8mb4'男') or (`sex` = _utf8mb4'女'))
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of t_user
-- ----------------------------
INSERT INTO `t_user` VALUES ('1001', '张三', '男', '111');
INSERT INTO `t_user` VALUES ('1002', '李四', '男', '222');
INSERT INTO `t_user` VALUES ('1003', '西蒙尼', '男', '333');
INSERT INTO `t_user` VALUES ('1004', '姆巴佩', '男', '444');
INSERT INTO `t_user` VALUES ('1005', '迪丽热巴', '女', '555');

-- ----------------------------
-- Event structure for update_overtime_status
-- ----------------------------
DROP EVENT IF EXISTS `update_overtime_status`;
delimiter ;;
CREATE EVENT `update_overtime_status`
ON SCHEDULE
EVERY '1' HOUR STARTS '2024-05-26 17:53:33'
DO UPDATE t_lend
  SET overtime = CASE 
                    WHEN DATEDIFF(NOW(), datetime) > 7 THEN '已超期'
                    ELSE '未超期'
                 END
;;
delimiter ;

-- ----------------------------
-- Event structure for update_overtime_status_all
-- ----------------------------
DROP EVENT IF EXISTS `update_overtime_status_all`;
delimiter ;;
CREATE EVENT `update_overtime_status_all`
ON SCHEDULE
EVERY '1' HOUR STARTS '2024-05-28 11:35:25'
DO UPDATE t_lend_all
  SET overtime = CASE 
                    WHEN DATEDIFF(NOW(), datetime) > 7 THEN '已超期' 
                    ELSE '未超期'
                 END
;;
delimiter ;

-- ----------------------------
-- Triggers structure for table t_lend
-- ----------------------------
DROP TRIGGER IF EXISTS `before_insert_t_lend`;
delimiter ;;
CREATE TRIGGER `before_insert_t_lend` BEFORE INSERT ON `t_lend` FOR EACH ROW BEGIN
    IF DATEDIFF(NOW(), NEW.datetime) > 7 THEN
        SET NEW.overtime = '已超期';
    ELSE
        SET NEW.overtime = '未超期';
    END IF;
END
;;
delimiter ;

-- ----------------------------
-- Triggers structure for table t_lend
-- ----------------------------
DROP TRIGGER IF EXISTS `before_update_t_lend`;
delimiter ;;
CREATE TRIGGER `before_update_t_lend` BEFORE UPDATE ON `t_lend` FOR EACH ROW BEGIN
    IF DATEDIFF(NOW(), NEW.datetime) > 7 THEN
        SET NEW.overtime = '已超期';
    ELSE
        SET NEW.overtime = '未超期';
    END IF;
END
;;
delimiter ;

-- ----------------------------
-- Triggers structure for table t_lend_all
-- ----------------------------
DROP TRIGGER IF EXISTS `before_insert_t_lend_all`;
delimiter ;;
CREATE TRIGGER `before_insert_t_lend_all` BEFORE INSERT ON `t_lend_all` FOR EACH ROW BEGIN
    IF DATEDIFF(NOW(), NEW.datetime) > 7 THEN
        SET NEW.overtime = '已超期';
    ELSE
        SET NEW.overtime = '未超期';
    END IF;
END
;;
delimiter ;

-- ----------------------------
-- Triggers structure for table t_lend_all
-- ----------------------------
DROP TRIGGER IF EXISTS `before_update_t_lend_all`;
delimiter ;;
CREATE TRIGGER `before_update_t_lend_all` BEFORE UPDATE ON `t_lend_all` FOR EACH ROW BEGIN
    IF DATEDIFF(NOW(), NEW.datetime) > 7 THEN
        SET NEW.overtime = '已超期';
    ELSE
        SET NEW.overtime = '未超期';
    END IF;
END
;;
delimiter ;

SET FOREIGN_KEY_CHECKS = 1;
