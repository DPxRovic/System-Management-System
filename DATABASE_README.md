# Database Schema Documentation

## Quick Reference

This document provides a quick reference for the database schema of the System Management System, with emphasis on the subject-based tracking features.

## Table Overview

| Table Name | Purpose | Key Relationships |
|------------|---------|-------------------|
| `users` | Authentication and authorization | - |
| `students` | Student records | - |
| `faculty` | Legacy faculty records | Referenced by `courses` |
| `professors` | Enhanced faculty with subject tracking | Referenced by `enrollments`, `professor_sections`, `professor_subjects` |
| `courses` | Course definitions | References `faculty`, referenced by `enrollments`, `attendance` |
| `enrollments` | Student enrollments with subject/professor tracking | References `courses`, `professors` |
| `attendance` | Attendance records | References `courses` |
| `sections` | Class sections organized by subject | Referenced by `professor_sections` |
| `professor_sections` | Professor-section assignments per subject | References `professors`, `sections` |
| `professor_subjects` | Professor subject qualifications | References `professors` |

## Key Features

### Subject-Based Tracking
- **Primary Key**: `subject_code` (VARCHAR(20))
- Used in: `professors`, `sections`, `professor_sections`, `professor_subjects`, `enrollments`

### Capacity Management
- **Section Capacity**: `sections.capacity` (INT, DEFAULT 30)
- Tracks maximum students per section

### Direct Relationships
- **Student-Professor-Subject**: Via `enrollments` table with `professor_id` and `subject_code`
- **Professor-Section-Subject**: Via `professor_sections` table
- **Professor-Subject Qualification**: Via `professor_subjects` table

## Common Queries

### 1. Get all professors teaching a specific subject
```sql
SELECT DISTINCT p.* 
FROM professors p
INNER JOIN professor_subjects ps ON p.id = ps.professor_id
WHERE ps.subject_code = 'CS101'
AND ps.status = 'Active';
```

### 2. Get students enrolled with a professor for a subject
```sql
SELECT s.*, e.enrollment_date, e.grade
FROM students s
INNER JOIN enrollments e ON s.student_id = e.student_id
WHERE e.professor_id = 1 
AND e.subject_code = 'CS101'
AND e.status = 'Enrolled';
```

### 3. Check section capacity and availability
```sql
SELECT 
    sec.section_code,
    sec.section_name,
    sec.subject_code,
    sec.capacity,
    COUNT(DISTINCT e.student_id) as enrolled_count,
    (sec.capacity - COUNT(DISTINCT e.student_id)) as available_slots
FROM sections sec
LEFT JOIN enrollments e ON e.subject_code = sec.subject_code
WHERE sec.status = 'Active'
GROUP BY sec.id, sec.section_code, sec.section_name, sec.subject_code, sec.capacity;
```

### 4. Get all sections taught by a professor
```sql
SELECT s.*, ps.subject_code
FROM sections s
INNER JOIN professor_sections ps ON s.id = ps.section_id
WHERE ps.professor_id = 1
AND ps.status = 'Active'
AND s.status = 'Active';
```

### 5. Get professor workload (subjects and sections)
```sql
SELECT 
    p.professor_id,
    p.name,
    COUNT(DISTINCT ps.subject_code) as subjects_taught,
    COUNT(DISTINCT psec.section_id) as sections_assigned
FROM professors p
LEFT JOIN professor_subjects ps ON p.id = ps.professor_id AND ps.status = 'Active'
LEFT JOIN professor_sections psec ON p.id = psec.professor_id AND psec.status = 'Active'
WHERE p.status = 'Active'
GROUP BY p.id, p.professor_id, p.name;
```

## Data Insertion Order

Due to foreign key constraints, data must be inserted in the following order:

1. **Independent tables**:
   - `users`
   - `students`
   - `faculty`
   - `professors`
   - `sections`

2. **Dependent tables**:
   - `courses` (depends on `faculty`)
   - `professor_subjects` (depends on `professors`)
   - `professor_sections` (depends on `professors`, `sections`)
   - `enrollments` (depends on `courses`, `professors`)
   - `attendance` (depends on `courses`)

## Data Deletion Order

To properly handle foreign key constraints, delete in reverse order:

1. `professor_subjects`
2. `professor_sections`
3. `attendance`
4. `enrollments`
5. `sections`
6. `courses`
7. `students`
8. `professors`
9. `faculty`
10. `users`

## Migration Notes

When upgrading an existing database:

1. The schema will automatically create new tables if they don't exist
2. Existing `enrollments` table will be enhanced with new columns:
   - `subject_code` (VARCHAR(20))
   - `professor_id` (INT)
3. New indexes will be added automatically
4. Foreign key constraint for `professor_id` will be added automatically
5. **Important**: Populate `professors` table before adding foreign key constraints to `enrollments`

## Validation

Use the `schema_validation.sql` script to validate the schema:

```bash
mysql -u username -p database_name < schema_validation.sql
```

This will verify:
- All new tables exist
- New columns in enrollments table
- All indexes are properly created
- Foreign key constraints are in place
- Unique constraints are working

## Backward Compatibility

- The `faculty` table remains unchanged
- Existing `courses` table structure is preserved
- New features are additive, not replacing existing functionality
- Applications can migrate gradually from `faculty` to `professors` table

## Best Practices

1. **Always use transactions** when inserting related data across multiple tables
2. **Check section capacity** before enrolling students
3. **Validate professor-subject relationship** in `professor_subjects` before assignment
4. **Use subject_code consistently** across all related tables
5. **Set appropriate ON DELETE actions**:
   - CASCADE for junction tables
   - SET NULL for optional relationships

## Indexes

All tables include appropriate indexes for:
- Primary keys (auto-indexed)
- Foreign keys (indexed for join performance)
- Subject codes (frequently queried)
- Status fields (for filtering)
- Composite indexes for multi-column queries

## Further Documentation

- See `SCHEMA_CHANGES.md` for detailed change documentation
- See `DatabaseInitializer.vb` for implementation details
- See inline comments in code for specific column purposes
