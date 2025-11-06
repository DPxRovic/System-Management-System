-- ==========================================
-- Database Schema Validation Script
-- Purpose: Validate the new subject-based tracking schema
-- Created: 2025-11-06
-- ==========================================

-- This script can be used to validate that all tables, columns, indexes, and constraints
-- have been properly created according to the schema changes.

-- ==========================================
-- 1. Verify New Tables Exist
-- ==========================================

-- Check for professors table
SELECT 
    'professors' as table_name,
    COUNT(*) as exists_count
FROM information_schema.tables 
WHERE table_schema = DATABASE() 
AND table_name = 'professors'

UNION ALL

-- Check for sections table
SELECT 
    'sections' as table_name,
    COUNT(*) as exists_count
FROM information_schema.tables 
WHERE table_schema = DATABASE() 
AND table_name = 'sections'

UNION ALL

-- Check for professor_sections table
SELECT 
    'professor_sections' as table_name,
    COUNT(*) as exists_count
FROM information_schema.tables 
WHERE table_schema = DATABASE() 
AND table_name = 'professor_sections'

UNION ALL

-- Check for professor_subjects table
SELECT 
    'professor_subjects' as table_name,
    COUNT(*) as exists_count
FROM information_schema.tables 
WHERE table_schema = DATABASE() 
AND table_name = 'professor_subjects';

-- ==========================================
-- 2. Verify New Columns in Enrollments Table
-- ==========================================

SELECT 
    column_name,
    data_type,
    column_type,
    is_nullable,
    column_key
FROM information_schema.columns
WHERE table_schema = DATABASE()
AND table_name = 'enrollments'
AND column_name IN ('subject_code', 'professor_id')
ORDER BY column_name;

-- ==========================================
-- 3. Verify Indexes on New Tables
-- ==========================================

-- Professors table indexes
SELECT 
    'professors' as table_name,
    index_name,
    column_name,
    seq_in_index
FROM information_schema.statistics
WHERE table_schema = DATABASE()
AND table_name = 'professors'
AND index_name IN ('idx_professor_id', 'idx_subject_code', 'idx_department', 'idx_status')
ORDER BY index_name, seq_in_index;

-- Sections table indexes
SELECT 
    'sections' as table_name,
    index_name,
    column_name,
    seq_in_index
FROM information_schema.statistics
WHERE table_schema = DATABASE()
AND table_name = 'sections'
AND index_name IN ('idx_section_code', 'idx_subject_code', 'idx_status')
ORDER BY index_name, seq_in_index;

-- Professor_sections table indexes
SELECT 
    'professor_sections' as table_name,
    index_name,
    column_name,
    seq_in_index
FROM information_schema.statistics
WHERE table_schema = DATABASE()
AND table_name = 'professor_sections'
AND index_name IN ('idx_professor_id', 'idx_section_id', 'idx_subject_code', 'idx_composite')
ORDER BY index_name, seq_in_index;

-- Professor_subjects table indexes
SELECT 
    'professor_subjects' as table_name,
    index_name,
    column_name,
    seq_in_index
FROM information_schema.statistics
WHERE table_schema = DATABASE()
AND table_name = 'professor_subjects'
AND index_name IN ('idx_professor_id', 'idx_subject_code', 'idx_status')
ORDER BY index_name, seq_in_index;

-- Enrollments table new indexes
SELECT 
    'enrollments' as table_name,
    index_name,
    column_name,
    seq_in_index
FROM information_schema.statistics
WHERE table_schema = DATABASE()
AND table_name = 'enrollments'
AND index_name IN ('idx_subject_code', 'idx_professor_id', 'idx_composite_enrollment')
ORDER BY index_name, seq_in_index;

-- ==========================================
-- 4. Verify Foreign Key Constraints
-- ==========================================

SELECT 
    constraint_name,
    table_name,
    column_name,
    referenced_table_name,
    referenced_column_name
FROM information_schema.key_column_usage
WHERE table_schema = DATABASE()
AND (
    (table_name = 'enrollments' AND constraint_name LIKE '%professor%')
    OR table_name = 'professor_sections'
    OR table_name = 'professor_subjects'
)
AND referenced_table_name IS NOT NULL
ORDER BY table_name, constraint_name;

-- ==========================================
-- 5. Verify Unique Constraints
-- ==========================================

SELECT 
    table_name,
    index_name,
    GROUP_CONCAT(column_name ORDER BY seq_in_index) as columns
FROM information_schema.statistics
WHERE table_schema = DATABASE()
AND table_name IN ('professor_sections', 'professor_subjects')
AND non_unique = 0
AND index_name NOT IN ('PRIMARY')
GROUP BY table_name, index_name
ORDER BY table_name, index_name;

-- ==========================================
-- 6. Sample Data Insertion Test (for manual testing)
-- ==========================================
/*
-- Uncomment to test data insertion

-- Insert sample professor
INSERT INTO professors (professor_id, name, department, email, subject_code, status)
VALUES ('PROF001', 'Dr. Test Professor', 'Computer Science', 'test@example.com', 'CS101', 'Active');

-- Insert sample section
INSERT INTO sections (section_code, section_name, subject_code, capacity, status)
VALUES ('SEC001', 'Section A', 'CS101', 30, 'Active');

-- Link professor to section
INSERT INTO professor_sections (professor_id, section_id, subject_code, status)
VALUES (
    (SELECT id FROM professors WHERE professor_id = 'PROF001'),
    (SELECT id FROM sections WHERE section_code = 'SEC001'),
    'CS101',
    'Active'
);

-- Add professor subject
INSERT INTO professor_subjects (professor_id, subject_code, subject_name, status)
VALUES (
    (SELECT id FROM professors WHERE professor_id = 'PROF001'),
    'CS101',
    'Introduction to Computer Science',
    'Active'
);

-- Verify insertions
SELECT * FROM professors WHERE professor_id = 'PROF001';
SELECT * FROM sections WHERE section_code = 'SEC001';
SELECT * FROM professor_sections;
SELECT * FROM professor_subjects;

-- Clean up test data
DELETE FROM professor_sections WHERE subject_code = 'CS101';
DELETE FROM professor_subjects WHERE subject_code = 'CS101';
DELETE FROM sections WHERE section_code = 'SEC001';
DELETE FROM professors WHERE professor_id = 'PROF001';
*/

-- ==========================================
-- 7. Schema Summary
-- ==========================================

SELECT 
    'Schema Validation Summary' as description,
    (SELECT COUNT(*) FROM information_schema.tables WHERE table_schema = DATABASE() AND table_name IN ('professors', 'sections', 'professor_sections', 'professor_subjects')) as new_tables_count,
    (SELECT COUNT(*) FROM information_schema.columns WHERE table_schema = DATABASE() AND table_name = 'enrollments' AND column_name IN ('subject_code', 'professor_id')) as new_columns_count;
