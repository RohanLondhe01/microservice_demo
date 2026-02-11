// api/courses.js
import { http } from "./http";

/**
 * Returns an array of courses regardless of server shape.
 * Supports:
 *   - { courses: [...] }
 *   - [...]
 * Falls back to [] on any mismatch or error.
 */
export const getAllCoursesApi = async () => {
  const res = await http.get("/courses"); // AxiosResponse
  const data = res?.data;

  if (Array.isArray(data?.courses)) return data.courses; // envelope shape
  if (Array.isArray(data)) return data;                   // raw array shape
  return [];                                              // normalize
};

export const getCourseBySlugApi = async (slug) => {
  const res = await http.get(`/courses/${slug}`);
  // If your backend returns { course: {...} }, prefer that; otherwise return the raw data.
  return res?.data?.course ?? res?.data ?? null;
};

export const createCourseApi = (payload) => http.post("/courses", payload);
export const updateCourseApi = (id, payload) => http.put(`/courses/${id}`, payload);
export const deleteCourseApi = (id) => http.delete(`/courses/${id}`);