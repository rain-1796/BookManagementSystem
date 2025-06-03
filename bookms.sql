/*
 Navicat Premium Data Transfer

 Source Server         : demo
 Source Server Type    : MySQL
 Source Server Version : 80404 (8.4.4)
 Source Host           : localhost:3306
 Source Schema         : bookms

 Target Server Type    : MySQL
 Target Server Version : 80404 (8.4.4)
 File Encoding         : 65001

 Date: 03/06/2025 18:31:26
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for t_admin
-- ----------------------------
DROP TABLE IF EXISTS `t_admin`;
CREATE TABLE `t_admin`  (
  `id` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `psw` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Records of t_admin
-- ----------------------------
INSERT INTO `t_admin` VALUES ('001', '9A648EBAA9BFB28DA88986A64C8955D5');
INSERT INTO `t_admin` VALUES ('002', 'B27D11B72DF93817F66FC5D4083C9F9F');
INSERT INTO `t_admin` VALUES ('003', '5BBD3CC28E9D25F982460136480D01FB');

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
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Records of t_book
-- ----------------------------
INSERT INTO `t_book` VALUES ('9787020162987', '三国演义', 4, '罗贯中', '人民文学出版社', 10);
INSERT INTO `t_book` VALUES ('9787302521426', '西游记', 4, '吴承恩', '清华大学出版社', 10);
INSERT INTO `t_book` VALUES ('9787521752236', '史蒂夫·乔布斯传', 5, '沃尔特·艾萨克森', '中信出版社', 19);
INSERT INTO `t_book` VALUES ('9787559602152', '明朝那些事儿', 3, '当年明月', '北京联合出版社', 17);
INSERT INTO `t_book` VALUES ('9787532763696', '卡拉马佐夫兄弟', 1, '陀思妥耶夫斯基', '上海译文出版社', 25);
INSERT INTO `t_book` VALUES ('9787536692930', '三体1：地球往事', 2, '刘慈欣', '重庆出版社', 23);
INSERT INTO `t_book` VALUES ('9787532776771', '挪威的森林', 1, '村上春树', '上海译文出版社', 8);
INSERT INTO `t_book` VALUES ('9787559478207', '沙丘', 2, '弗兰克·赫伯特', '江苏凤凰文艺出版社', 66);
INSERT INTO `t_book` VALUES ('2025', '新年快乐', 1, '小黑', '艰苦奋斗出版社', 55);
INSERT INTO `t_book` VALUES ('9787508678498', '历史的温度', 3, '张玮', '中信出版社', 77);

-- ----------------------------
-- Table structure for t_kind
-- ----------------------------
DROP TABLE IF EXISTS `t_kind`;
CREATE TABLE `t_kind`  (
  `id` int NOT NULL AUTO_INCREMENT,
  `kind` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 6 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = DYNAMIC;

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
  `reserve_time` datetime NULL DEFAULT NULL,
  `borrow_time` datetime NULL DEFAULT NULL,
  `should_return_time` datetime NULL DEFAULT NULL,
  `return_time` datetime NULL DEFAULT NULL,
  `status` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `overdue` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  PRIMARY KEY (`no` DESC) USING BTREE,
  INDEX `user_id`(`uid` ASC) USING BTREE,
  INDEX `book_id`(`bid` ASC) USING BTREE,
  CONSTRAINT `user_id` FOREIGN KEY (`uid`) REFERENCES `t_user` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 36 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Records of t_lend
-- ----------------------------
INSERT INTO `t_lend` VALUES (35, '1002', '9787512502352', '毛泽东传', '2025-03-16 21:14:56', NULL, NULL, NULL, '取消预约', NULL);
INSERT INTO `t_lend` VALUES (34, '1002', '9787302521426', '西游记', '2025-03-16 21:14:54', '2025-03-16 21:15:36', '2025-04-15 21:15:36', '2025-03-16 21:19:44', '已还书', '已超期');
INSERT INTO `t_lend` VALUES (33, '1004', '2025', '新年快乐', '2025-03-16 20:51:31', '2025-03-16 20:59:55', '2025-04-15 20:59:55', '2025-03-16 21:04:01', '已还书', '已超期');
INSERT INTO `t_lend` VALUES (32, '1004', '9787532763696', '卡拉马佐夫兄弟', '2025-03-16 20:51:27', '2025-03-16 21:10:43', '2025-04-15 21:10:43', NULL, '已借书', '已超期');
INSERT INTO `t_lend` VALUES (31, '1002', '9787536692930', '三体1：地球往事', '2025-01-07 22:18:58', '2025-01-07 22:30:23', '2025-02-06 22:30:23', '2025-01-07 22:33:07', '已还书', '已超期');
INSERT INTO `t_lend` VALUES (30, '1002', '9787508678498', '历史的温度', '2025-01-07 22:14:56', NULL, NULL, NULL, '取消预约', NULL);
INSERT INTO `t_lend` VALUES (29, '1001', '9787512502352', '毛泽东传', '2025-01-04 18:41:43', NULL, NULL, NULL, '取消预约', NULL);
INSERT INTO `t_lend` VALUES (28, '1001', '9787532776771', '挪威的森林', '2025-01-04 18:38:31', NULL, NULL, NULL, '取消预约', NULL);
INSERT INTO `t_lend` VALUES (27, '1001', '9787559602152', '明朝那些事儿', '2025-01-04 17:12:43', '2025-01-04 17:14:30', '2025-02-03 17:14:30', '2025-01-04 17:18:47', '已还书', '已超期');
INSERT INTO `t_lend` VALUES (26, '1001', '9787302521426', '西游记', '2025-01-04 17:12:39', '2025-01-04 17:14:03', '2025-02-03 17:14:03', '2025-01-04 17:18:56', '已还书', '已超期');
INSERT INTO `t_lend` VALUES (25, '1001', '9787508678498', '历史的温度', '2025-01-01 17:46:02', NULL, NULL, NULL, '取消预约', NULL);

-- ----------------------------
-- Table structure for t_user
-- ----------------------------
DROP TABLE IF EXISTS `t_user`;
CREATE TABLE `t_user`  (
  `id` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `name` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `sex` char(2) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `psw` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE,
  CONSTRAINT `sex` CHECK ((`sex` = _utf8mb4'男') or (`sex` = _utf8mb4'女'))
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Records of t_user
-- ----------------------------
INSERT INTO `t_user` VALUES ('1001', '张三', '男', '1222D73064760C097FFB229F2988B6DB');
INSERT INTO `t_user` VALUES ('1002', '李四', '男', 'B27D11B72DF93817F66FC5D4083C9F9F');
INSERT INTO `t_user` VALUES ('1003', '西蒙尼', '男', '5BBD3CC28E9D25F982460136480D01FB');
INSERT INTO `t_user` VALUES ('1004', '姆巴佩', '男', 'DB94A6597CCE582C0E47892F137AA9F7');
INSERT INTO `t_user` VALUES ('1005', '娜美', '女', 'C99F9D4126651BBBD8BC0C38D621064D');
INSERT INTO `t_user` VALUES ('1006', '莫利纳', '男', '858B97E2389ED13CCA9C2CA708BAE7D7');
INSERT INTO `t_user` VALUES ('1007', '路明非', '男', '9C61797F76E42D4233DEF30A8DB17C9E');
INSERT INTO `t_user` VALUES ('1008', '卡普', '男', '23D93FFD0AC819E8099E159822EFF2D9');

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

SET FOREIGN_KEY_CHECKS = 1;
