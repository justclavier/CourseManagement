-- phpMyAdmin SQL Dump
-- version 4.4.15.10
-- https://www.phpmyadmin.net
--
-- Host: 127.0.0.1:3306
-- Generation Time: 2023-12-17 20:41:54
-- 服务器版本： 5.7.30
-- PHP Version: 5.4.45

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `cus_mgt`
--

-- --------------------------------------------------------

--
-- 表的结构 `courses`
--

CREATE TABLE IF NOT EXISTS `courses` (
  `course_id` varchar(5) COLLATE utf8mb4_unicode_ci NOT NULL,
  `course_name` varchar(256) COLLATE utf8mb4_unicode_ci NOT NULL,
  `description` varchar(256) COLLATE utf8mb4_unicode_ci NOT NULL,
  `is_publish` int(1) NOT NULL,
  `course_cover` varchar(256) COLLATE utf8mb4_unicode_ci NOT NULL,
  `course_url` varchar(256) COLLATE utf8mb4_unicode_ci NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- 转存表中的数据 `courses`
--

INSERT INTO `courses` (`course_id`, `course_name`, `description`, `is_publish`, `course_cover`, `course_url`) VALUES
('C0001', 'C#/.Net架构师蜕变营', 'C#/.Net架构师蜕变营C#/.Net架构师蜕变营C#/.Net架构师蜕变营C#/.Net架构师蜕变营C#/.Net架构师蜕变营', 1, '/Assets/Images/validcode.png', 'https://ke.qq.com/course/456926'),
('C0002', 'C#/.Net高级进阶VIP班', 'C#/.Net高级进阶VIP班C#/.Net高级进阶VIP班C#/.Net高级进阶VIP班C#/.Net高级进阶VIP班C#/.Net高级进阶VIP班C#/.Net高级进阶VIP班C#/.Net高级进阶VIP班', 1, '/Assets/Images/avatar.png', 'https://ke.qq.com/course/453949'),
('C0003', 'Java高级实战VIP班', 'Java高级实战VIP班Java高级实战VIP班Java高级实战VIP班Java高级实战VIP班Java高级实战VIP班', 1, '/Assets/Images/validcode.png', 'https://ke.qq.com/course/2770287'),
('C0004', 'C#/.Net全栈VIP班', 'C#/.Net全栈VIP班C#/.Net全栈VIP班C#/.Net全栈VIP班C#/.Net全栈VIP班C#/.Net全栈VIP班C#/.Net全栈VIP班C#/.Net全栈VIP班', 1, '/Assets/Images/logo.jpg', 'https://ke.qq:com/course/464059'),
('C0005', 'Winform高级就业班', 'Winform高级就业班Winform高级就业班Winform高级就业班Winform高级就业班Winform高级就业班Winform高级就业班Winform高级就业班Winform高级就业班', 1, '/Assets/Images/validcode.png', 'https://ke.qq.com/course/464930');

-- --------------------------------------------------------

--
-- 表的结构 `course_teacher_relation`
--

CREATE TABLE IF NOT EXISTS `course_teacher_relation` (
  `course_id` varchar(20) COLLATE utf8mb4_unicode_ci NOT NULL,
  `teacher_id` varchar(20) COLLATE utf8mb4_unicode_ci NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- 转存表中的数据 `course_teacher_relation`
--

INSERT INTO `course_teacher_relation` (`course_id`, `teacher_id`) VALUES
('C0001', '1003'),
('C0001', '1005'),
('C0002', '1004'),
('C0002', '1005'),
('C0003', '1006'),
('C0004', '1007'),
('C0005', '1008');

-- --------------------------------------------------------

--
-- 表的结构 `platforms`
--

CREATE TABLE IF NOT EXISTS `platforms` (
  `platform_id` varchar(5) COLLATE utf8mb4_unicode_ci NOT NULL,
  `platform_name` varchar(10) COLLATE utf8mb4_unicode_ci NOT NULL,
  `is_validation` int(1) NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- 转存表中的数据 `platforms`
--

INSERT INTO `platforms` (`platform_id`, `platform_name`, `is_validation`) VALUES
('FF001', '云课堂', 1),
('FF002', 'B站', 1),
('FF003', '知乎', 1),
('FF004', '抖音', 1),
('FF005', '博客', 1),
('PF006', '其他', 1);

-- --------------------------------------------------------

--
-- 表的结构 `play_record`
--

CREATE TABLE IF NOT EXISTS `play_record` (
  `course_id` varchar(5) COLLATE utf8mb4_unicode_ci NOT NULL,
  `platform_id` varchar(5) COLLATE utf8mb4_unicode_ci NOT NULL,
  `play_count` decimal(18,0) NOT NULL,
  `is_growing` int(1) NOT NULL,
  `growing_rate` decimal(18,0) NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- 转存表中的数据 `play_record`
--

INSERT INTO `play_record` (`course_id`, `platform_id`, `play_count`, `is_growing`, `growing_rate`) VALUES
('C0001', 'FF001', '161', 0, '-75'),
('C0001', 'FF002', '283', 1, '4'),
('C0001', 'FF003', '568', 1, '22'),
('C0001', 'FF004', '41', 1, '77'),
('C0001', 'FF005', '678', 1, '91'),
('C0002', 'FF001', '207', 0, '-43'),
('C0002', 'FF002', '930', 0, '-64'),
('C0002', 'FF003', '210', 1, '6'),
('C0002', 'FF004', '107', 1, '4'),
('C0002', 'FF005', '420', 1, '31'),
('C0003', 'FF001', '512', 1, '25'),
('C0003', 'FF002', '921', 1, '86'),
('C0003', 'FF003', '161', 0, '-29'),
('C0003', 'FF004', '918', 0, '-96'),
('C0003', 'FF005', '590', 0, '-96'),
('C0004', 'FF001', '454', 1, '38'),
('C0004', 'FF002', '443', 0, '-75'),
('C0004', 'FF003', '68', 0, '-72'),
('C0004', 'FF004', '274', 0, '-40'),
('C0004', 'FF005', '678', 0, '-42'),
('C0005', 'FF001', '264', 1, '64'),
('C0005', 'FF002', '53', 0, '-64'),
('C0005', 'FF003', '638', 0, '-64'),
('C0005', 'FF004', '404', 1, '49'),
('C0005', 'FF005', '862', 1, '78'),
('C0001', 'PF006', '453', 1, '43');

-- --------------------------------------------------------

--
-- 表的结构 `users`
--

CREATE TABLE IF NOT EXISTS `users` (
  `user_id` varchar(20) COLLATE utf8mb4_unicode_ci NOT NULL,
  `user_name` varchar(20) COLLATE utf8mb4_unicode_ci NOT NULL,
  `real_name` varchar(20) COLLATE utf8mb4_unicode_ci NOT NULL,
  `password` varchar(40) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `is_validation` int(11) NOT NULL,
  `is_can_login` int(11) NOT NULL,
  `is_teacher` int(11) NOT NULL,
  `avatar` varchar(200) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `gender` int(11) DEFAULT NULL
) ENGINE=MyISAM DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- 转存表中的数据 `users`
--

INSERT INTO `users` (`user_id`, `user_name`, `real_name`, `password`, `is_validation`, `is_can_login`, `is_teacher`, `avatar`, `gender`) VALUES
('1002', 'guest', 'Guest', '2D64CDF22D0B162AC64F5F7A883DC964', 1, 0, 0, '/Assets/Images/avatar.png', 1),
('1001', 'admin', 'Administrator', '51A70A1E25F9E6A8954F54D6DF935B0D', 1, 1, 0, '/Assets/Images/avatar.png', 1),
('1003', 'eleven', 'Eleven', 'C95C977F4EFC60E2717BB730A69470F2', 1, 1, 1, '/Assets/Images/avatar.png', 1),
('1004', 'richard', 'Richard', 'EF60F453E23F1B9B3C45C97C5E1C316E', 1, 1, 1, '/Assets/Images/avatar.png', 1),
('1005', 'clay', 'Clay', '0E6AE0856E2CDD1E94344562EFF41A23', 1, 1, 1, '/Assets/Images/avatar.png', 1),
('1006', 'garry', 'Garry', '1FF74A56AE38F179E201BC91F754CBA1', 1, 1, 1, '/Assets/Images/avatar.png', 1),
('1007', 'ace', 'Ace', '1D4C08127C768A77A1E39CCA5E208F91', 1, 1, 1, '/Assets/Images/avatar.png', 1),
('1008', 'leah', 'Leah', '50A1CDDA6D8D09C9021FC490016240B4', 1, 1, 1, '/Assets/Images/avatar.png', 2),
('1009', 'jovan', 'Jovan', '3B9D859D7EF2C8EA60B890FEEFF20912', 1, 1, 1, '/Assets/Images/avatar.png', 1);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `courses`
--
ALTER TABLE `courses`
  ADD PRIMARY KEY (`course_id`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`user_id`);

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
