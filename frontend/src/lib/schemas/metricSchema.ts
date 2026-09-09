import { z } from 'zod';

export const metricSchema = z.object({
	metricName: z.string().min(2, 'Metric name must be at least 2 characters'),
	value: z.coerce.number().positive('Value must be a positive number'),
	tenantId: z.string().min(1, 'Tenant ID is required')
});

export type MetricSchema = typeof metricSchema;
