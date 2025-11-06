# Database Schema Changes - Subject-Based Tracking

## Overview
This document describes the database schema changes implemented on 2025-11-06 to enable proper subject-based relationship tracking between students, professors, and sections.

## New Tables

### 1. `professors` Table
Enhanced faculty tracking with subject_code support.

**Purpose:** Track professors and their associated subjects while maintaining backward compatibility with the existing faculty table.

**Columns:**
- `id` (INT, PRIMARY KEY, AUTO_INCREMENT)
- `professor_id` (VARCHAR(20), UNIQUE) - Unique identifier for professor
- `name` (VARCHAR(100), NOT NULL)
- `department` (VARCHAR(100))
- `email` (VARCHAR(100))
- `phone_number` (VARCHAR(20))
- `specialization` (VARCHAR(100))
- `subject_code` (VARCHAR(20)) - **NEW: Links professor to subject**
- `hire_date` (DATE, DEFAULT CURRENT_DATE)
- `status` (VARCHAR(20), DEFAULT 'Active')
- `created_at` (TIMESTAMP, DEFAULT CURRENT_TIMESTAMP)
- `updated_at` (TIMESTAMP, DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP)

**Indexes:**
- `idx_professor_id` on `professor_id`
- `idx_subject_code` on `subject_code`
- `idx_department` on `department`
- `idx_status` on `status`

### 2. `sections` Table
Organizes classes by subject with capacity tracking.

**Purpose:** Create sections for courses/subjects and track enrollment capacity.

**Columns:**
- `id` (INT, PRIMARY KEY, AUTO_INCREMENT)
- `section_code` (VARCHAR(20), UNIQUE) - Unique section identifier
- `section_name` (VARCHAR(100), NOT NULL)
- `subject_code` (VARCHAR(20), NOT NULL) - **Links section to subject**
- `capacity` (INT, DEFAULT 30) - **Maximum students per section**
- `schedule` (VARCHAR(100))
- `room` (VARCHAR(50))
- `status` (VARCHAR(20), DEFAULT 'Active')
- `created_at` (TIMESTAMP, DEFAULT CURRENT_TIMESTAMP)
- `updated_at` (TIMESTAMP, DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP)

**Indexes:**
- `idx_section_code` on `section_code`
- `idx_subject_code` on `subject_code`
- `idx_status` on `status`

### 3. `professor_sections` Table
Junction table linking professors to sections for specific subjects.

**Purpose:** Enable many-to-many relationship between professors and sections, explicitly tracking which professor teaches which section for which subject.

**Columns:**
- `id` (INT, PRIMARY KEY, AUTO_INCREMENT)
- `professor_id` (INT, NOT NULL) - Foreign key to professors table
- `section_id` (INT, NOT NULL) - Foreign key to sections table
- `subject_code` (VARCHAR(20), NOT NULL) - **Explicit subject tracking**
- `assigned_date` (DATE, DEFAULT CURRENT_DATE)
- `status` (VARCHAR(20), DEFAULT 'Active')
- `created_at` (TIMESTAMP, DEFAULT CURRENT_TIMESTAMP)
- `updated_at` (TIMESTAMP, DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP)

**Indexes:**
- `idx_professor_id` on `professor_id`
- `idx_section_id` on `section_id`
- `idx_subject_code` on `subject_code`
- `idx_composite` on `(professor_id, section_id, subject_code)`

**Constraints:**
- `UNIQUE KEY unique_assignment (professor_id, section_id, subject_code)` - Prevents duplicate assignments
- `FOREIGN KEY (professor_id) REFERENCES professors(id) ON DELETE CASCADE`
- `FOREIGN KEY (section_id) REFERENCES sections(id) ON DELETE CASCADE`

### 4. `professor_subjects` Table
Many-to-many junction table for professors and subjects.

**Purpose:** Track which subjects each professor is qualified to teach, independent of current section assignments.

**Columns:**
- `id` (INT, PRIMARY KEY, AUTO_INCREMENT)
- `professor_id` (INT, NOT NULL) - Foreign key to professors table
- `subject_code` (VARCHAR(20), NOT NULL)
- `subject_name` (VARCHAR(100), NOT NULL)
- `assigned_date` (DATE, DEFAULT CURRENT_DATE)
- `status` (VARCHAR(20), DEFAULT 'Active')
- `created_at` (TIMESTAMP, DEFAULT CURRENT_TIMESTAMP)
- `updated_at` (TIMESTAMP, DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP)

**Indexes:**
- `idx_professor_id` on `professor_id`
- `idx_subject_code` on `subject_code`
- `idx_status` on `status`

**Constraints:**
- `UNIQUE KEY unique_professor_subject (professor_id, subject_code)` - One entry per professor-subject pair
- `FOREIGN KEY (professor_id) REFERENCES professors(id) ON DELETE CASCADE`

## Modified Tables

### `enrollments` Table
Enhanced to include subject_code and professor_id for direct tracking of student-professor-subject relationships.

**New Columns:**
- `subject_code` (VARCHAR(20)) - **Links enrollment to specific subject**
- `professor_id` (INT) - **Direct link to the professor for this enrollment**

**New Indexes:**
- `idx_subject_code` on `subject_code`
- `idx_professor_id` on `professor_id`
- `idx_composite_enrollment` on `(student_id, course_id, subject_code)`

**New Constraints:**
- `FOREIGN KEY (professor_id) REFERENCES professors(id) ON DELETE SET NULL` - Maintains referential integrity

## Migration Support

The `UpdateTableStructure` method has been enhanced to automatically add missing columns and indexes to existing tables:

### For `enrollments` table:
1. Adds `subject_code` column if missing
2. Adds `professor_id` column if missing
3. Adds indexes for new columns
4. Adds foreign key constraint for professor_id

### New Helper Methods:
1. **`AddIndexIfNotExists`** - Safely adds indexes without duplication
2. **`AddForeignKeyIfNotExists`** - Safely adds foreign key constraints with validation

## Benefits

1. **Clear Subject Tracking:** Direct association between students, professors, and subjects through multiple relationship paths
2. **Section Management:** Proper capacity tracking and organization by subject
3. **Flexible Assignment:** Professors can teach multiple subjects and sections
4. **Data Integrity:** Foreign key constraints ensure referential integrity
5. **Backward Compatibility:** Existing `faculty` table remains unchanged
6. **Migration Ready:** Automatic schema updates for existing databases

## Relationship Diagram

```
students ─── enrollments ─── professors
              │    │              │
              │    └─ subject_code
              │
           courses
              
professors ─── professor_subjects (subject_code, subject_name)
    │
    └───── professor_sections ─── sections (subject_code, capacity)
                   │
              subject_code
```

## Usage Examples

### Finding all professors teaching a specific subject:
```sql
SELECT p.* FROM professors p
INNER JOIN professor_subjects ps ON p.id = ps.professor_id
WHERE ps.subject_code = 'CS101'
```

### Finding all students enrolled with a specific professor for a subject:
```sql
SELECT s.* FROM students s
INNER JOIN enrollments e ON s.student_id = e.student_id
WHERE e.professor_id = 1 AND e.subject_code = 'CS101'
```

### Finding section capacity and enrollment:
```sql
SELECT 
    sec.section_code,
    sec.section_name,
    sec.capacity,
    COUNT(e.id) as enrolled_count,
    (sec.capacity - COUNT(e.id)) as available_slots
FROM sections sec
LEFT JOIN enrollments e ON e.subject_code = sec.subject_code
WHERE sec.id = 1
GROUP BY sec.id
```

## Notes for Developers

1. The `faculty` table is maintained for backward compatibility
2. New development should use the `professors` table for subject-based tracking
3. Foreign key constraints require proper order when inserting data:
   - Create professors before enrollments
   - Create sections before professor_sections
4. When dropping tables, use the order specified in `DropAllTables()` to respect foreign key constraints
5. All new tables include proper indexing for performance optimization
